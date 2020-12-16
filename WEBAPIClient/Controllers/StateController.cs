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
    public class StateController : BaseApiController
    {
        #region Fields
        private readonly StateBLL stateBLL;
        #endregion Fields

        #region Constructors
        public StateController()
        {
            this.stateBLL = new StateBLL();
        }
        #endregion Constructors


        [HttpGet]
        [Authorize]
        [Route("api/states/get")]
        public IEnumerable<StateViewModel> Get()
        {
            return this.stateBLL.Get();
        }
    }
}