using BLL.BLLs;
using Institucional.WebApi;
using Microsoft.Owin.Testing;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Utilities;
using ViewModels.ViewModels;
using WebApi.App_Start;

namespace WebApi.Controllers
{
    public class LoginController : BaseApiController
    {

        // POST api/user/login
        [HttpPost]
        [AllowAnonymous]
        [Route("api/login")]
        public async Task<IHttpActionResult> Login(UserViewModel userViewModel)
        {
            try
            {
                UserBLL userBLL = new UserBLL();
                if (userViewModel == null)
                {
                    throw new Exception(Messages.InvalidLoginRequest);
                }
                var user = userBLL.LogIn(userViewModel);
                
                // Invoke the "token" OWIN service to perform the login (POST /api/token)
                var testServer = TestServer.Create<Startup>();
                var requestParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", userViewModel.UserName),
                    new KeyValuePair<string, string>("password", userViewModel.Password)
                };
                var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
                var tokenServiceResponse = await testServer.HttpClient.PostAsync(
                    "/api/token", requestParamsFormUrlEncoded);


                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, user);
                string token = tokenServiceResponse.Content.ReadAsStringAsync().Result;
                // Set headers for paging
                response.Headers.Add("Authorization", token);

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