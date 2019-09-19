using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class BinnacleViewModel : BaseViewModel
    {
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public UserViewModel User { get; set; }
    }
}
