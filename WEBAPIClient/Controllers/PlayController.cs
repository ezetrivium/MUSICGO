using BLL.BLLs;
using Institucional.WEBAPIClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Utilities;
using ViewModels.ViewModels;

namespace WEBAPIClient.Controllers
{
    public class PlayController : BaseApiController
    {
        [HttpGet]
        [Route("api/sample/getsample")]
        [AllowAnonymous]
        public HttpResponseMessage GetAudioContent(string songKey, Guid songID)
        {
            SongBLL songBLL = new SongBLL();

            songBLL.CountPlayback(new SongViewModel() { Id = songID});

            var audio = new AudioStream(songKey);

            var httpResponse = Request.CreateResponse();
            httpResponse.Content = new PushStreamContent(audio.WriteToStream, new MediaTypeHeaderValue("audio/wav"));
            return httpResponse;
        }
       
    }
}