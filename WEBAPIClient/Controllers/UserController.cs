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


        [HttpPost]
        [Authorize(Roles = "AddUser")]
        [Route("api/users/add")]
        public void Post([FromBody]UserViewModel user)
        {

        }
    }
}