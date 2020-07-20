using BLL.BLLs;
using Institucional.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using ViewModels.ViewModels;

namespace WebApi.Controllers
{
    public class UserController : BaseApiController
    {
        #region Fields
        private readonly UserBLL userBLL;
        #endregion Fields

        #region Constructors
        public UserController()
        {
            this.userBLL = new UserBLL();
        }
        #endregion Constructors

        // GET: api/Servicio

        [HttpGet]
        [Authorize(Roles = "GetUsers")]
        [Route("api/users/get")]
        public IEnumerable<UserViewModel> Get()
        {
            return this.userBLL.Get();
        }
    }
}