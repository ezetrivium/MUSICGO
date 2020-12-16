using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class VoteViewModel : BaseViewModel
    {
        public bool Positive { get; set; }

        public SongViewModel Song { get; set; }

        public int TimeToPositive { get; set; }

        public UserViewModel User { get; set; }
    }
}
