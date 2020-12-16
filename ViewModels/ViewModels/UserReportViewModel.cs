using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class UserReportViewModel: BaseViewModel
    {
        public List<UserViewModel> Users { get; set; }

        public int TotalUsers { get; set; }


        public int InactiveUsers { get; set; }

    }
}
