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
    public class LanguageBLL : BaseBLL<LanguageBE,LanguageViewModel>
    {
        public LanguageBLL()
        {
            Dal = new LanguageDAL();
        }

        public LanguageBE GetUserLanguage(UserBE user)
        {
            LanguageDAL languadeDal = new LanguageDAL();
            return languadeDal.GetById(user.Language.Id);
        }
    }
}
