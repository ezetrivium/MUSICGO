using BE.Entities;
using DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ViewModels;

namespace BLL.BLLs
{
    public class ServiceBLL : BaseBLL<ServiceBE, ServiceViewModel>
    {
        public ServiceBLL()
        {
            this.Dal = new ServiceDAL();
        }
    }
}
