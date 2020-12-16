using BLL.BLLs;
using Institucional.WEBAPIClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Utilities;
using Utilities.Exceptions;
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


        [HttpGet]
        [Authorize(Roles = "GetPermissions")]
        [Route("api/permissions/get")]
        public IEnumerable<PermissionViewModel> Get()
        {
            return this.permissionBLL.Get();
        }


        [HttpGet]
        [Authorize(Roles = "GetPermissions")]
        [Route("api/permissions/getgroups")]
        public IEnumerable<PermissionViewModel> GetGroups()
        {
            return this.permissionBLL.GetPermissionsGroups();
        }


        [HttpGet]
        [Authorize(Roles = "GetPermissions")]
        [Route("api/permissions/getchilds")]
        public IEnumerable<PermissionViewModel> GetChilds()
        {
            return this.permissionBLL.GetChildPermissions();
        }


        [HttpPost]
        [Authorize(Roles = "CreatePermission")]
        [Route("api/permissions/add")]
        public IHttpActionResult Post(PermissionsParentViewModel permissionsParentView)
        {
            try
            {


                var result = this.permissionBLL.Add(permissionsParentView.Child,permissionsParentView.Parent);

                if(result == Guid.Empty)
                {
                    throw new BusinessException(Messages.ErrorAddPermission);
                }

                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, Messages.SuccessfulUperationCrud);

                return this.ResponseMessage(response);
            }
            catch (BusinessException ex)
            {
                var response = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
                return this.ResponseMessage(response);
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, Messages.Generic_Error);
                return this.ResponseMessage(response);
            }

        }


        [HttpPut]
        [Authorize(Roles = "UpdatePermission")]
        [Route("api/permissions/update")]
        public IHttpActionResult Put(PermissionViewModel viewModel)
        {
            try
            {


                var result = this.permissionBLL.Update(viewModel);

                if (!result)
                {
                    throw new BusinessException(Messages.ErrorUpdatePermission);
                }

                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, Messages.SuccessfulUperationCrud);

                return this.ResponseMessage(response);
            }
            catch (BusinessException ex)
            {
                var response = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
                return this.ResponseMessage(response);
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, Messages.Generic_Error);
                return this.ResponseMessage(response);
            }

        }


        [HttpPost]
        [Authorize(Roles = "CreatePermission")]
        [Route("api/permissionsgroup/add")]
        public IHttpActionResult PostPermissionsGroup(PermissionsParentViewModel permissionsParentView)
        {
            try
            {


                var result = this.permissionBLL.AddPermissionGroup(permissionsParentView.Child,permissionsParentView.Parent);

                if (!result)
                {
                    throw new BusinessException(Messages.ErrorAddPermissionGroup);
                }

                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, Messages.SuccessfulUperationCrud);

                return this.ResponseMessage(response);
            }
            catch (BusinessException ex)
            {
                var response = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
                return this.ResponseMessage(response);
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, Messages.Generic_Error);
                return this.ResponseMessage(response);
            }

        }

    }
}