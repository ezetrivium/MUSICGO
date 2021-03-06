﻿using BE.Entities;
using BLL.BLLs;

using Institucional.WEBAPIClient;
using Microsoft.Owin.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Utilities;
using ViewModels.ViewModels;
using WEBAPIClient.App_Start;

namespace WEBAPIClient.Controllers
{
    public class LoginController : BaseApiController
    {

        // POST api/user/login
        [HttpPost]
        [AllowAnonymous]
        [Route("api/login")]
        public UserViewModel Login(UserViewModel userViewModel)
        {
    
            UserBLL userBLL = new UserBLL();
                if (userViewModel == null)
                {
                    throw new Exception(Messages.InvalidLoginRequest);
                }
                var user = userBLL.LogIn(userViewModel);

            //CacheManager.Set(user.UserName, user);

                // Invoke the "token" OWIN service to perform the login (POST /api/token)
                //var testServer = TestServer.Create<Startup>();
                //var requestParams = new List<KeyValuePair<string, string>>
                //{
                //    new KeyValuePair<string, string>("grant_type", "password"),
                //    new KeyValuePair<string, string>("username", userViewModel.UserName),
                //    new KeyValuePair<string, string>("password", userViewModel.Password)
                //};
                //var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
                //var tokenServiceResponse = await testServer.HttpClient.PostAsync(
                //    "/api/token", requestParamsFormUrlEncoded);



                //var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, user);
                //string token = tokenServiceResponse.Content.ReadAsStringAsync().Rsesult;
                //// Set headers for paging
                //response.Headers.Add("Authorization", token);
                //this.ResponseMessage(response);

                return user;
        
          



        }



        [HttpPost]
        [AllowAnonymous]
        [Route("api/logout")]
        public void Logout()
        {
            BinnacleSL binnacleSL = new BinnacleSL();
            var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;

            string userObj = identityClaims.FindFirst("userObject").Value;

            binnacleSL.AddBinnacle(new BinnacleBE()
            {
                User = JsonConvert.DeserializeObject<UserBE>(userObj),
                Description = "Login",
            });


        }
    }
}