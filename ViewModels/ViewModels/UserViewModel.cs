using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class UserViewModel: BaseViewModel
    {
        public UserViewModel()
        {
            Permissions = new List<PermissionViewModel>();
        }
        public string DVH { get; set; }

        public Boolean Blocked { get; set; }

        public LanguageViewModel Language { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string ArtistName { get; set; }

        public int Playbacks { get; set; }

        public IEnumerable<PermissionViewModel> Permissions { get; set; }
    }
}
