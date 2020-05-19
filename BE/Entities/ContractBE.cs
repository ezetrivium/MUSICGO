using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities
{
    public class ContractBE : BaseEntity
    {
        public DateTime HireDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public ServiceBE Service { get; set; }

        public UserBE User { get; set; }
    }
}
