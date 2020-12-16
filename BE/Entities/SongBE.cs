using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities
{
    public class SongBE : BaseEntity
    {
        public AlbumBE Album { get; set; }

        public CategoryBE Category { get; set; }

        public string Name { get; set; }

        public int Playbacks {get;set;}

        public string SongKey { get; set; }

        public DateTime UploadDate { get; set; }

        public UserBE User { get; set; }

        public List<VoteBE> Votes { get; set; }

        public int VotesCount { get; set; }
    }
}
