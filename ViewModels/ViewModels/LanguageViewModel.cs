using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class LanguageViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public Dictionary<string, string> Dictionary { get; set; }
    }
}
