using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class ClaimViewModel : BaseViewModel
    {
        public string Description { get; set; }

        public SongViewModel SongClaimed { get; set; }

        public StateViewModel State { get; set; }

        public UserViewModel User { get; set; }

        public DateTime Date { get; set; }
    }
}
