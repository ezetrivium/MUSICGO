using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities
{
    public class ClaimBE : BaseEntity
    {
        public string Description { get; set; }

        public SongBE SongClaimed { get; set; }

        public StateBE State { get; set; }

        public UserBE User { get; set; }

        public DateTime Date { get; set; }


    }
}
