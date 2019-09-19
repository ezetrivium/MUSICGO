using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities
{
    public class DVVBE : BaseEntity
    {
        public string TableName { get; set; }

        public string DVVHash { get; set; }
    }
}
