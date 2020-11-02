using BE.Entities;
using DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using ViewModels.ViewModels;

namespace BLL.BLLs
{
    public class ContractBLL : BaseBLL<ContractBE, ContractViewModel>
    {

        public ContractBLL()
        {
            this.Dal = new ContractDAL();
        }

        protected override bool IsValid(ContractViewModel viewModel)
        {

            if (viewModel.Service.Id != Guid.Empty &&
                viewModel.User.Id != Guid.Empty)
            {      
                    return true;

            }
            else
            {
                return false;
            }
        }


        public override Guid Add(ContractViewModel viewModel)
        {
            throw new NotImplementedException();

        }
    }
}
