using BE.Entities;
using BLL.BLLs;
using Institucional.WEBAPIClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using Utilities;
using Utilities.Exceptions;
using ViewModels;
using ViewModels.ViewModels;

namespace WEBAPIClient.Controllers
{
    public class SongController : BaseApiController
    {

        #region Fields
        private readonly SongBLL songBLL;
        #endregion Fields

        #region Constructors
        public SongController()
        {
            this.songBLL = new SongBLL();
        }
        #endregion Constructors


        [HttpGet]
        [Authorize(Roles = "GetSongs")]
        [Route("api/songs/get")]
        public IEnumerable<SongViewModel> Get(bool refresh)
        {
            IList<SongViewModel> songs = null;
            var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
            string userObj = identityClaims.FindFirst("userObject").Value;
            var userLogged = JsonConvert.DeserializeObject<UserBE>(userObj);
            var uvm = Mapper.Map<UserBE, UserViewModel>(userLogged);

            if (CacheManager.GetWithTimeout("songs") == null || refresh)
            {
                songs = this.songBLL.Get();
                CacheManager.SetWithTimeout("songs", songs, TimeSpan.FromHours(12));
            }
            else
            {
                songs = CacheManager.Get("songs") as List<SongViewModel>;
            }
            return songs;
        }

        [HttpGet]
        [Authorize(Roles = "GetSongs, GetUserSongs")]
        [Route("api/songs/getbyid")]
        public SongViewModel GetByID(Guid id)
        {
            return this.songBLL.GetById(id);
        }


        [HttpGet]
        [Authorize(Roles = "GetUserSongs")]
        [Route("api/songs/getuser")]
        public UserViewModel GetUserSongs()
        {
            var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
            string userObj = identityClaims.FindFirst("userObject").Value;
            var userLogged= JsonConvert.DeserializeObject<UserBE>(userObj);
            var uvm = Mapper.Map<UserBE,UserViewModel>(userLogged);
            return this.songBLL.GetUserSongs(uvm);
        }


        [HttpGet]
        [Authorize(Roles = "GetSongs")]
        [Route("api/songs/getreport")]
        public IEnumerable<SongViewModel> GetSongsReport()
        {

            return this.songBLL.GetReport();
        }

        [HttpPost]
        [Authorize(Roles = "CalculateSuccess")]
        [Route("api/songs/calculatesuccess")]
        public IHttpActionResult CalculateSuccess(SongViewModel songViewModel)
        {
            
            try
            {
                if (songViewModel != null)
                {
                    var song = this.songBLL.GetById(songViewModel.Id);
                    
                    var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, this.songBLL.CalculateSuccess(song));
                    return this.ResponseMessage(response);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, Messages.Generic_Error);
                return this.ResponseMessage(response);
            }

        }


        //[HttpPost]
        //[Authorize(Roles = "CalculateSuccess")]
        //[Route("api/songs/returnfile")]
        //public IHttpActionResult ReturnFile(SongViewModel songViewModel)
        //{

        //    try
        //    {

        //        if (songViewModel != null)
        //        {
                   

                 

        //            var result = new HttpResponseMessage(HttpStatusCode.OK)
        //            {
        //                Content = new ByteArrayContent(File.ReadAllBytes(FileUtils.GetRepoMusicPath(songViewModel.SongKey)))
        //            };
        //            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
        //            {
        //                FileName = songViewModel.Name + ".mp3"
        //            };
        //            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

        //            var response = ResponseMessage(result);

        //            return response;
                    
        //        }
        //        else
        //        {
        //            throw new Exception();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var response = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, Messages.Generic_Error);
        //        return this.ResponseMessage(response);
        //    }

        //}



        [HttpGet]
        [Authorize(Roles = "Play")]
        [Route("api/songs/toplay")]
        public IEnumerable<SongViewModel> GetSongsToPlay(bool refresh)
        {
            var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
            string userObj = identityClaims.FindFirst("userObject").Value;
            var userLogged = JsonConvert.DeserializeObject<UserBE>(userObj);
            var uvm = Mapper.Map<UserBE, UserViewModel>(userLogged);
            IList<SongViewModel> songs = null;

            if (CacheManager.GetWithTimeout(uvm.Id+"songsplay") == null || refresh)
            {
                songs = this.songBLL.GetSongsToPlay(uvm);
                CacheManager.SetWithTimeout(uvm.Id + "songsplay", songs, TimeSpan.FromHours(12));
            }
            else
            {
                songs = CacheManager.Get(uvm.Id + "songsplay") as List<SongViewModel>;
            }

            return songs;
        }


        [HttpGet]
        [Authorize(Roles = "Play")]
        [Route("api/songs/voted")]
        public IEnumerable<SongViewModel> GetSongsVoted()
        {
            var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
            string userObj = identityClaims.FindFirst("userObject").Value;
            var userLogged = JsonConvert.DeserializeObject<UserBE>(userObj);
            var uvm = Mapper.Map<UserBE, UserViewModel>(userLogged);
 

            return this.songBLL.GetSongsVoted(uvm);
        }



        [HttpPost]
        [Authorize(Roles = "AddSong")]
        [Route("api/songs/add")]
        public IHttpActionResult Post()
        {
            try
            {

                var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
                string userObj = identityClaims.FindFirst("userObject").Value;
                var userLogged = JsonConvert.DeserializeObject<UserBE>(userObj);
                var uvm = Mapper.Map<UserBE, UserViewModel>(userLogged);

                var songviewmodel = new SongViewModel();

                songviewmodel = JsonConvert.DeserializeObject<SongViewModel>(HttpContext.Current.Request.Params["song"]);

                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    songviewmodel.File = new HttpPostedFileWrapper(HttpContext.Current.Request.Files[0]);
                }
                else
                {
                    throw new BusinessException(Messages.InvalidSongFileRequest);
                }


                songviewmodel.User = uvm;

                var result = this.songBLL.Add(songviewmodel);

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
        [Route("api/songs/update")]
        public IHttpActionResult Put(SongViewModel songViewModel)
        {
            try
            {


                var result = this.songBLL.Update(songViewModel);

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
        [Route("api/songs/delete/{id:Guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {


                var result = this.songBLL.Delete(id);

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