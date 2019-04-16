using SiparisTakip.Dal.Abstract;
using SiparisTakip.Entittes.Models;
using SiparisTakip.Entittes.PocoModels;
using SiparisTakip.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisTakip.Bll
{
    public class KullaniciManager : GenericManager<Kullanici>, IKullaniciService
    {
        IKullaniciRepository kullaniciRepository;

        public KullaniciManager(IKullaniciRepository kullaniciRepository) : base(kullaniciRepository)
        {
            this.kullaniciRepository = kullaniciRepository;
        }

        public PocoKullanici KullaniciGiris(string kullaniciAdi, string parola)
        {

            if (string.IsNullOrEmpty(kullaniciAdi.Trim()))
            {
                throw new Exception("Kullanıcı Adı Boş Geçilemez.");
            }

            if (string.IsNullOrEmpty(parola.Trim()))
            {
                throw new Exception("Parola Boş Geçilemez.");
            }

            var sifre = new ToPasswordRepository().Md5(parola);
            var kullanici = kullaniciRepository.KullaniciGiris(kullaniciAdi, sifre);

            if(kullanici==null)
            {
                throw new Exception("Kullanıcı Adınızı veya Parolanızı Kontrol Ediniz.");
            }
            else
            {
                return new PocoKullanici()
                {
                    KullaniciID = kullanici.KullaniciID,
                    KullaniciAdi = kullanici.KullaniciAdi,
                    KullaniciSoyadi = kullanici.KullaniciSoyadi,
                    YetkiID = kullanici.YetkiID
                };
            }
        }
    }
}
