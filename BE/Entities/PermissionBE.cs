using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities
{
    public class PermissionBE : BaseEntity
    {
        public string Name { get; set; }


        public bool Check(string name)
        {
            if(name == Name)
            {
                return true;
            }
            return false;
        }
    }
}
