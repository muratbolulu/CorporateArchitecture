using SiparisTakip.Cache;
using SiparisTakip.Dal.Concrete.EntityFramework.Repository;
using SiparisTakip.Entittes.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisTakip.Bll
{
    public class CacheFonsiyon
    {
        DefaultCacheProvider provider = new DefaultCacheProvider();
        public void CacheTemizle()
        {
            provider.Remove(Enums.CacheKey.Kategoriler.ToString());
            provider.Remove(Enums.CacheKey.OlcuBirimi.ToString());
        }

        public void CacheOlustur()
        {
            #region Kategori
            object kategoriCache = null;
            try
            {
                GenericManager<Tanim> genericManager = new GenericManager<Tanim>(new EfGenericRepository<Tanim>());

                var kategori = genericManager.GetAll(x => x.TanimGrupID == (int)Enums.TanimGrup.StokGrubu);

                if (kategori != null)
                    kategoriCache = kategori;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }

            provider.Set(Enums.CacheKey.Kategoriler.ToString(), kategoriCache);
            #endregion

            #region Ölçü Birimi
            object olcuBirimiCache = null;
            try
            {
                GenericManager<Tanim> genericManager = new GenericManager<Tanim>(new EfGenericRepository<Tanim>());

                var olcuBirim = genericManager.GetAll(x => x.TanimGrupID == (int)Enums.TanimGrup.OlcuBirim);

                if (olcuBirim != null)
                    olcuBirimiCache = olcuBirim;
                else
                    throw new Exception("Ölçü Birimi Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Ölçü Birimi Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Ölçü Birimi Cache' Doldurulamadı.", error);
            }

            provider.Set(Enums.CacheKey.OlcuBirimi.ToString(), olcuBirimiCache);

            #endregion
        }

        public object KategoriGetir()
        {
            object value = null;
            try
            {
                var kategori = (List<Tanim>)(provider.Get(Enums.CacheKey.Kategoriler.ToString()));

                if (kategori != null)
                    value = kategori;
            }
            catch (Exception error)
            {
                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı."+error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);
            }

            return value;
        }

        public object CacheTanimGetir(string key)
        {
            object value = null;
            try
            {
                var tanim = (List<Tanim>)(provider.Get(key));

                if (tanim != null)
                    value = tanim;
            }
            catch (Exception error)
            {
                value = null;
                Trace.WriteLine(key+ " Cache'ten Okunamadı." + error.Message);
                throw new Exception(key+" Cache'ten Okunamadı.", error);
            }

            return value;
        }
    }
}
