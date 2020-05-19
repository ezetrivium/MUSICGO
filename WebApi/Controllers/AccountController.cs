using BLL.BLLs;
using Institucional.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Utilities;
using ViewModels.ViewModels;

namespace WebApi.Controllers
{
    public class AccountController : BaseApiController
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("api/account/recoverpassword")]
        public async Task<IHttpActionResult> RecoverPassword(UserViewModel userViewModel)
        {
            try
            {
                UserBLL userBLL = new UserBLL();
                if (userViewModel == null)
                {
                    throw new Exception(Messages.InvalidResetPasswordRequest);
                }

                await userBLL.RecoverPassword(userViewModel);

                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK,Messages.SuccessfulResetPasswordRequest);

                return this.ResponseMessage(response);
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
                return this.ResponseMessage(response);
            }



        }




        [HttpPost]
        [AllowAnonymous]
        [Route("api/account/updatepassword")]
        public IHttpActionResult UpdatePassword(RecoverPasswordViewModel updatePasswordViewModel)
        {
            try
            {
                UserBLL userBLL = new UserBLL();
                if (updatePasswordViewModel == null)
                {
                    throw new Exception(Messages.InvalidUpdatePasswordRequest);
                }

                userBLL.UpdatePassword(updatePasswordViewModel);

                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, Messages.SuccessfulUpdatePassword);

                return this.ResponseMessage(response);
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
                return this.ResponseMessage(response);
            }



        }
    }
}