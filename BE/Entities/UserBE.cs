using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities
{
    public class UserBE : BaseEntity
    {
      
        public UserBE()
        {
            Permissions = new List<PermissionBE>();
        }
        public string DVH { get; set; }

        public bool Blocked { get; set; }

        public LanguageBE Language { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string ArtistName { get; set; }

        public int Playbacks { get; set; }

        public IList<PermissionBE> Permissions { get; set; }


        public ContractBE Contract { get; set; }

        public string ImgKey { get; set; }

        public DateTime? BlockedDateTime { get; set; }


        public List<SongBE> Songs { get; set; }


        public List<AlbumBE> Albums { get; set; }

        
    }
}
