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
    public class VoteBLL : BaseBLL<VoteBE, VoteViewModel>
    {
        public VoteBLL()
        {
            this.Dal = new VoteDAL();
        }

        public IList<VoteBE> GetUserVotes(UserViewModel user)
        {
            VoteDAL voteDAL = new VoteDAL();
            var userbe = Mapper.Map<UserViewModel, UserBE>(user);

            var votes = voteDAL.GetUserVotes(userbe);

            return votes;
        }

    }
}
