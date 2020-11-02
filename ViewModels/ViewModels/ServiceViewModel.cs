using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class ServiceViewModel : BaseViewModel
    {
        public string Description { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Code { get; set; }
    }
}
