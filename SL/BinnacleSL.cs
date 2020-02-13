using BE.Entities;
using DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using ViewModels.ViewModels;

namespace SL
{
    public class BinnacleSL
    {
        public IList<BinnacleViewModel> GetBinnacle(DateTime? dateTo, DateTime? dateFrom, string userName)
        {
            var binnacleDAL = new BinnacleDAL();
            IList<BinnacleBE> binnaclesBE = binnacleDAL.GetBinnacleWithFilters(dateTo, dateFrom, userName);
            IList<BinnacleViewModel> viewModels = Mapper.Map<BinnacleBE, BinnacleViewModel>(binnaclesBE);
            return viewModels;
        }

        public void AddBinnacle(BinnacleBE binnacle)
        {
            var binnacleDAL = new BinnacleDAL();
            binnacleDAL.Add(binnacle);
        }
    }
}
