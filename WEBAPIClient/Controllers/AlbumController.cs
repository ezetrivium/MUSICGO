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
    public class AlbumController : BaseApiController
    {
        #region Fields
        private readonly AlbumBLL albumBLL;
        #endregion Fields

        #region Constructors
        public AlbumController()
        {
            this.albumBLL = new AlbumBLL();
        }
        #endregion Constructors



        [HttpGet]
        [Authorize(Roles = "GetUserSongs")]
        [Route("api/albums/getuser")]
        public UserViewModel GetUserAlbums()
        {
            var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
            string userObj = identityClaims.FindFirst("userObject").Value;
            var userLogged = JsonConvert.DeserializeObject<UserBE>(userObj);
            var uvm = Mapper.Map<UserBE, UserViewModel>(userLogged);
            uvm= this.albumBLL.GetUserAlbums(uvm);
            return uvm;
        }


        [HttpPost]
        [Authorize(Roles = "AddSong")]
        [Route("api/albums/add")]
        public IHttpActionResult Post()
        {
            try
            {
                var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
                string userObj = identityClaims.FindFirst("userObject").Value;
                var userLogged = JsonConvert.DeserializeObject<UserBE>(userObj);
                var uvm = Mapper.Map<UserBE, UserViewModel>(userLogged);

                var albumviewmodel = new AlbumViewModel();

                albumviewmodel = JsonConvert.DeserializeObject<AlbumViewModel>(HttpContext.Current.Request.Params["album"]);

                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    albumviewmodel.File = new HttpPostedFileWrapper(HttpContext.Current.Request.Files[0]);
                }
                else
                {
                    throw new BusinessException(Messages.InvalidImageRequest);
                }


                albumviewmodel.User = uvm;

                var result = this.albumBLL.Add(albumviewmodel);

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
        [Authorize(Roles = "UpdateSong")]
        [Route("api/albums/update")]
        public IHttpActionResult Put()
        {
            try
            {

                var albumviewmodel = new AlbumViewModel();

                albumviewmodel = JsonConvert.DeserializeObject<AlbumViewModel>(HttpContext.Current.Request.Params["album"]);

                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    albumviewmodel.File = new HttpPostedFileWrapper(HttpContext.Current.Request.Files[0]);
                }



                var result = this.albumBLL.Update(albumviewmodel);

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
        [Authorize(Roles = "DeleteSong")]
        [Route("api/albums/delete/{id:Guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {


                var result = this.albumBLL.Delete(id);

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