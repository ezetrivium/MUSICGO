using BE.Entities;
using BLL.BLLs;
using Institucional.WEBAPIClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using Utilities;
using Utilities.Exceptions;
using ViewModels.ViewModels;

namespace WEBAPIClient.Controllers
{
    public class ClaimController : BaseApiController
    {
        #region Fields
        private readonly ClaimBLL claimBLL;
        #endregion Fields

        #region Constructors
        public ClaimController()
        {
            this.claimBLL = new ClaimBLL();
        }
        #endregion Constructors


        [HttpGet]
        [Authorize(Roles = "GetClaims")]
        [Route("api/claims/get")]
        public IEnumerable<ClaimViewModel> Get()
        {
            return this.claimBLL.Get();
        }

        [HttpGet]
        [Authorize(Roles = "GetUserClaims")]
        [Route("api/claims/getuser")]
        public IEnumerable<ClaimViewModel> GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
            string userObj = identityClaims.FindFirst("userObject").Value;
            var userLogged = JsonConvert.DeserializeObject<UserBE>(userObj);
            var uvm = Mapper.Map<UserBE, UserViewModel>(userLogged);
            return this.claimBLL.GetUserClaims(uvm);
        }



        [HttpPost]
        [Authorize(Roles = "AddClaims")]
        [Route("api/claim/add")]
        public IHttpActionResult Post(ClaimViewModel claimViewModel)
        {
            try
            {

                var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
                string userObj = identityClaims.FindFirst("userObject").Value;
                var userLogged = JsonConvert.DeserializeObject<UserBE>(userObj);
                var uvm = Mapper.Map<UserBE, UserViewModel>(userLogged);

                claimViewModel.User = uvm;

                StateBLL stateBLL = new StateBLL();

                var states = stateBLL.Get();

                claimViewModel.State = states.Where(s => s.Name == "pending").First();

                var result = this.claimBLL.Add(claimViewModel);

                if (result == Guid.Empty)
                {
                    throw new BusinessException(Messages.ErrorAddClaim);
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
        [Authorize(Roles = "UpdateClaims")]
        [Route("api/claim/update")]
        public IHttpActionResult Put(ClaimViewModel claimViewModel)
        {
            try
            {




                var result = this.claimBLL.Update(claimViewModel);

                if (!result)
                {
                    throw new BusinessException(Messages.ErrorUpdateClaim);
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