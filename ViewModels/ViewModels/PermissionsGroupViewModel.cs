using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class PermissionsGroupViewModel : PermissionViewModel
    {

        public PermissionsGroupViewModel()
        {
            Permissions = new List<PermissionViewModel>();
        }

        public IList<PermissionViewModel> Permissions { get; set; }

    }
}
