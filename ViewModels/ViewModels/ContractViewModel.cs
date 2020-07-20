using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class ContractViewModel : BaseViewModel
    {

        public DateTime HireDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public ServiceViewModel Service { get; set; }

        public UserViewModel User { get; set; }
    }
}
