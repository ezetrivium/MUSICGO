using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ViewModels.ViewModels
{
    public class AlbumViewModel : BaseViewModel
    {
        public string ImgKey { get; set; }

        public string Name { get; set; }

        public UserViewModel User { get; set; }


        public DateTime UploadDate { get; set; }


        public List<SongViewModel> Songs { get; set; }

        public HttpPostedFileBase File { get; set; }

        public string ImageBase64 { get; set; }
    }
}
