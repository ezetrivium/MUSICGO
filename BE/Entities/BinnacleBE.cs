using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities
{
    public class BinnacleBE : BaseEntity
    {
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public UserBE User { get; set; }
    }
}
