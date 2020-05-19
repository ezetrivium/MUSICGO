using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities
{
    public class ServiceBE : BaseEntity
    {

        public string Description { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }
    }
}
