using BE.Entities;
using BLL.BLLs;

using Institucional.WEBAPIClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using Utilities;
using Utilities.Exceptions;
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
        public IEnumerable<UserViewModel> Get(bool refresh)
        {
            IList<UserViewModel> users = null;
            var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
            string userObj = identityClaims.FindFirst("userObject").Value;
            var userLogged = JsonConvert.DeserializeObject<UserBE>(userObj);
            var uvm = Mapper.Map<UserBE, UserViewModel>(userLogged);

            if (CacheManager.GetWithTimeout("users") == null || refresh)
            {
                users = this.userBLL.Get();
                CacheManager.SetWithTimeout("users", users, TimeSpan.FromHours(12));
            }
            else
            {
                users = CacheManager.Get("users") as List<UserViewModel>;
            }
            return users;
        }


        [HttpGet]
        [Authorize(Roles = "GetUsers")]
        [Route("api/users/getbyid")]
        public UserViewModel GetByID(Guid id)
        {
            return this.userBLL.GetById(id);
        }


        [HttpGet]
        [Authorize]
        [Route("api/users/getprofile")]
        public IHttpActionResult GetProfile(Guid id)
        {

            try
            {
                var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
                string userObj = identityClaims.FindFirst("userObject").Value;
                var userOld = JsonConvert.DeserializeObject<UserBE>(userObj);
                if (id != userOld.Id)
                {
                    throw new Exception();
                }

                var user =  this.userBLL.GetById(id);
                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, user);
                return this.ResponseMessage(response);

            }
            catch(Exception ex)
            {
                var response = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, Messages.Generic_Error);
                return this.ResponseMessage(response);
            }
            

        }


        [HttpGet]
        [Authorize(Roles = "GetUsers")]
        [Route("api/users/getreport")]
        public UserReportViewModel GetUsersReport()
        {

            return this.userBLL.GetReport();
        }


        [HttpPost]
        [Authorize(Roles = "AddUser")]
        [Route("api/users/setuserpermissions")]
        public IHttpActionResult Post(UserViewModel userViewModel)
        {
            try
            {
                

                var result = this.userBLL.SetUserPermissions(userViewModel);
                if (!result)
                {
                    throw new BusinessException(Messages.ErrorSetPermission);
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
        [Authorize(Roles = "AddUser")]
        [Route("api/users/add")]
        public IHttpActionResult Post()
        {
            try
            {
                var userviewmodel = new UserViewModel();
               
                userviewmodel = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Current.Request.Params["user"]);

                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    userviewmodel.File = new HttpPostedFileWrapper(HttpContext.Current.Request.Files[0]);
                }
                


                if (userviewmodel.File == null)
                {
                    userviewmodel.ImgKey = FileUtils.GetStandardImagePath();
                }

                if (this.userBLL.Add(userviewmodel) == Guid.Empty)
                {
                    throw new BusinessException(Messages.ErrorAddUser);
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
        [AllowAnonymous]
        [Route("api/users/subscribe")]
        public IHttpActionResult Subscribe()
        {
            try
            {
                var userviewmodel = new UserViewModel();

                userviewmodel = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Current.Request.Params["user"]);

                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    userviewmodel.File = new HttpPostedFileWrapper(HttpContext.Current.Request.Files[0]);
                }



                if (userviewmodel.File == null)
                {
                    userviewmodel.ImgKey = FileUtils.GetStandardImagePath();
                }

                Guid guidUser = this.userBLL.Add(userviewmodel);
                if (guidUser == Guid.Empty)
                {
                    throw new BusinessException(Messages.ErrorSubscription);
                }

                
                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, guidUser);

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




        [HttpDelete]
        [Authorize(Roles = "DeleteUser")]
        [Route("api/users/delete/{id:Guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                var result = this.userBLL.Delete(id);

                if (!result)
                {
                    throw new Exception(Messages.ErrorDeleteUser);
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



        [HttpDelete]
        [Authorize]
        [Route("api/users/deletemyprofile/{id:Guid}")]
        public IHttpActionResult DeleteMyProfile(Guid id)
        {
            try
            {

                var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
                string userObj = identityClaims.FindFirst("userObject").Value;
                var userOld = JsonConvert.DeserializeObject<UserBE>(userObj);
                if (id != userOld.Id)
                {
                    throw new Exception();
                }


                var result = this.userBLL.Delete(id);

                if (!result)
                {
                    throw new Exception(Messages.ErrorDeleteUser);
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
        [Authorize(Roles = "UpdateUser")]
        [Route("api/users/update")]
        public IHttpActionResult Put()
        {
            try
            {
                var userviewmodel = new UserViewModel();

                userviewmodel = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Current.Request.Params["user"]);

                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    userviewmodel.File = new HttpPostedFileWrapper(HttpContext.Current.Request.Files[0]);
                }



                if (!this.userBLL.Update(userviewmodel))
                {
                    throw new BusinessException(Messages.ErrorUpdateUser);
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
        [Authorize]
        [Route("api/users/myprofile")]
        public IHttpActionResult myProfile()
        {
            try
            {
                var userviewmodel = new UserViewModel();

                userviewmodel = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Current.Request.Params["user"]);

                var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
                string userObj = identityClaims.FindFirst("userObject").Value;
                var userOld = JsonConvert.DeserializeObject<UserBE>(userObj);



                if (userOld.Id != userviewmodel.Id)
                {
                    throw new Exception();
                }

                

                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    userviewmodel.File = new HttpPostedFileWrapper(HttpContext.Current.Request.Files[0]);
                }



                if (!this.userBLL.Update(userviewmodel))
                {
                    throw new BusinessException(Messages.ErrorUpdateUser);
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