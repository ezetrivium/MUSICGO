using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ViewModels.ViewModels
{
    public class SongViewModel : BaseViewModel
    {

        public AlbumViewModel Album { get; set; }

        public CategoryViewModel Category { get; set; }

        public string Name { get; set; }

        public int Playbacks { get; set; }

        public string SongKey { get; set; }

        public DateTime UploadDate { get; set; }

        public UserViewModel User { get; set; }

        public List<VoteViewModel> Votes { get; set; }

        public HttpPostedFileBase File { get; set; }

        public byte[] SongBytes { get; set; }

        public int VotesCount { get; set; }
    }
}
