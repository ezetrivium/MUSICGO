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
using ViewModels.ViewModels;

namespace WEBAPIClient.Controllers
{
    public class VoteController : BaseApiController
    {
        #region Fields
        private readonly VoteBLL voteBLL;
        #endregion Fields

        #region Constructors
        public VoteController()
        {
            this.voteBLL = new VoteBLL();
        }
        #endregion Constructors

        [HttpPost]
        [Authorize(Roles = "AddVote")]
        [Route("api/votes/add")]
        public IHttpActionResult Post(VoteViewModel voteViewModel)
        {
            try
            {
                var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
                string userObj = identityClaims.FindFirst("userObject").Value;
                var userLogged = JsonConvert.DeserializeObject<UserBE>(userObj);
                var uvm = Mapper.Map<UserBE, UserViewModel>(userLogged);

                voteViewModel.User = uvm;


                var result = this.voteBLL.Add(voteViewModel);

                if (result == Guid.Empty)
                {
                    throw new Exception(Messages.Generic_Error);
                }

                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, Messages.SuccessfulUperationCrud);

                return this.ResponseMessage(response);
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, Messages.Generic_Error);
                return this.ResponseMessage(response);
            }

        }



        [HttpGet]
        [Authorize(Roles = "GetUserVotes")]
        [Route("api/votes/getuser")]
        public IList<VoteViewModel> GetUserVotes()
        {
            var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
            string userObj = identityClaims.FindFirst("userObject").Value;
            var userLogged = JsonConvert.DeserializeObject<UserBE>(userObj);
            var uvm = Mapper.Map<UserBE, UserViewModel>(userLogged);
            var votes = Mapper.Map<VoteBE, VoteViewModel>(this.voteBLL.GetUserVotes(uvm));
            return votes;
        }
    }
}