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
    public class CategoryController : BaseApiController
    {
        #region Fields
        private readonly CategoryBLL categoryBLL;
        #endregion Fields

        #region Constructors
        public CategoryController()
        {
            this.categoryBLL = new CategoryBLL();
        }
        #endregion Constructors

        [HttpGet]
        [Authorize]
        [Route("api/categories/get")]
        public IEnumerable<CategoryViewModel> Get()
        {
            return this.categoryBLL.Get();
        }
    }
}