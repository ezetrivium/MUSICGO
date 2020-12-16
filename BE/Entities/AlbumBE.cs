using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities
{
    public class AlbumBE : BaseEntity
    {
        public string ImgKey { get; set; }

        public string Name { get; set; }

        public UserBE User { get; set; }


        public DateTime UploadDate { get; set; }


        public List<SongBE> Songs { get; set; }
    }
}
