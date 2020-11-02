using BLL.BLLs;
using Institucional.WEBAPIClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ViewModels.ViewModels;

namespace WEBAPIClient.Controllers
{
    public class ServiceController : BaseApiController
    {
        #region Fields
        private readonly ServiceBLL serviceBLL;
        #endregion Fields

        #region Constructors
        public ServiceController()
        {
            this.serviceBLL = new ServiceBLL();
        }
        #endregion Constructors

        [HttpGet]
       
        [Route("api/services/get")]
        public IEnumerable<ServiceViewModel> Get()
        {
            return this.serviceBLL.Get();
        }



       
    }
}