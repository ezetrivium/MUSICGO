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
    public class PermissionController : BaseApiController
    {
        #region Fields
        private readonly PermissionsBLL permissionBLL;
        #endregion Fields

        #region Constructors
        public PermissionController()
        {
            this.permissionBLL = new PermissionsBLL();
        }
        #endregion Constructors

        [HttpGet]
        [Authorize]
        [Route("api/permissions/getroots")]
        public IEnumerable<PermissionViewModel> GetRoots()
        {
            return this.permissionBLL.GetRootPermissions();
        }

    }
}