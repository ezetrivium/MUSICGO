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
    public class ClaimBLL : BaseBLL<ClaimBE, ClaimViewModel>
    {

        public ClaimBLL()
        {
            Dal = new ClaimDAL();
        }


        public IList<ClaimViewModel> GetUserClaims(UserViewModel user)
        {
            try
            {
                ClaimDAL claimDAL = new ClaimDAL();
                IList<ClaimBE> entities;
                var uservm = Mapper.Map<UserViewModel, UserBE>(user);
                entities = claimDAL.GetUserClaims(uservm);
                return Mapper.Map<ClaimBE, ClaimViewModel>(entities);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       



        protected override bool IsValid(ClaimViewModel viewModel)
        {

            if (viewModel.User.Id != Guid.Empty && 
                viewModel.SongClaimed.Id != Guid.Empty &&
                viewModel.State.Id != Guid.Empty &&
                viewModel.Description.Length > 0 &&
                viewModel.Description.Length < 4001)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

    }
}
