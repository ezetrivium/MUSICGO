using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities
{
    public class PermissionsGroupBE : PermissionBE
    {
        public PermissionsGroupBE()
        {
            Permissions = new List<PermissionBE>();
        }

        public IList<PermissionBE> Permissions { get; set; }
    }
}
