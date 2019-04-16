using SiparisTakip.Dal.Abstract;
using SiparisTakip.Entittes.Models;
using SiparisTakip.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisTakip.Bll
{
    public class CariManager : GenericManager<Cari>, ICariService
    {
        ICariRepository cariRepository;

        public CariManager(ICariRepository cariRepository):base(cariRepository)
        {
            this.cariRepository = cariRepository;
        }

        public List<Cari> CariHesapEkstersi(int cariId)
        {
            return cariRepository.CariHesapEktresi(cariId);
        }
    }
}
