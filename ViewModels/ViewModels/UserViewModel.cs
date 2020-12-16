using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ViewModels.ViewModels
{
    public class UserViewModel: BaseViewModel
    {
        public UserViewModel()
        {
            Permissions = new List<PermissionViewModel>();
        }


        public LanguageViewModel Language { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string ArtistName { get; set; }

        public int Playbacks { get; set; }

        public bool Blocked { get; set; }

        public IList<PermissionViewModel> Permissions { get; set; }

        public ContractViewModel Contract { get; set; }

        public string ImgKey { get; set; }

        public HttpPostedFileBase File { get; set; }

        public string ImageBase64 { get; set; }


        public List<SongViewModel> Songs { get; set; }


        public List<AlbumViewModel> Albums { get; set; }
    }
}
