using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class PermissionViewModel : BaseViewModel
    {
        public PermissionViewModel()
        {
            Permissions = new List<PermissionViewModel>();
        }

        public string Name { get; set; }

        public IEnumerable<PermissionViewModel> Permissions { get; set; }
    }
}
