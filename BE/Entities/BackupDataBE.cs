using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities
{
    public class BackupDataBE : BaseEntity
    {
        public DateTime Date { get; set; }

        public string Path { get; set; }

    }
}
