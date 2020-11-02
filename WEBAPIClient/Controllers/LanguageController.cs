
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
    public class LanguageController : BaseApiController
    {
        #region Fields
        private readonly LanguageBLL languageBLL;
        #endregion Fields

        #region Constructors
        public LanguageController()
        {
            this.languageBLL = new LanguageBLL();
        }
        #endregion Constructors

        [HttpGet]
        
        [Route("api/languages/get")]
        public IEnumerable<LanguageViewModel> Get()
        {
            return this.languageBLL.Get();
        }
    }
}