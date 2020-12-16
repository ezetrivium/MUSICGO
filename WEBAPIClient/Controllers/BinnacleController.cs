using Institucional.WEBAPIClient;
using SL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ViewModels.ViewModels;

namespace WEBAPIClient.Controllers
{
    public class BinnacleController : BaseApiController
    {
        #region Fields
        private readonly BinnacleSL binnacleSL;
        #endregion Fields

        #region Constructors
        public BinnacleController()
        {
            this.binnacleSL = new BinnacleSL();
        }
        #endregion Constructors


        [HttpGet]
        [Authorize(Roles = "GetBinnacle")]
        [Route("api/binnacle/get")]
        public IEnumerable<BinnacleViewModel> Get(DateTime? dateTo, DateTime? dateFrom, string userName )
        {
            return this.binnacleSL.GetBinnacle(dateTo,dateFrom,userName);
        }
    }
}