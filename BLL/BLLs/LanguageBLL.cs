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
    public class LanguageBLL : BaseBLL<LanguageBE, LanguageViewModel>
    {
        public LanguageBLL()
        {
            Dal = new LanguageDAL();
        }

        public Dictionary<Guid, string> GetDictionaryKeys(Guid LanguageID)
        {
            LanguageDAL languadeDal = new LanguageDAL();

            return languadeDal.GetDictionaryKeys(LanguageID);
        }


        public Dictionary<Guid, string> GetDictionaryKeysAll()
        {
            LanguageDAL languadeDal = new LanguageDAL();

            return languadeDal.GetDictionaryKeysAll();
        }


        public LanguageViewModel GetLanguageByCode(string code)
        {
            LanguageBE entity = null;
            LanguageDAL languadeDal = new LanguageDAL();

            if (CacheManager.GetWithTimeout("language-" + code) == null)
            {
                entity = languadeDal.GetByCode(code);
                CacheManager.SetWithTimeout("language-" + code, entity, TimeSpan.FromDays(1));
            }
            else
            {
                entity = CacheManager.Get("language-" + code) as LanguageBE;
            }


            return Mapper.Map<LanguageBE, LanguageViewModel>(entity);

        }




        public bool AddDictionary(LanguageViewModel viewModel)
        {
            try
            {
                LanguageDAL languageDAL = new LanguageDAL();
                CacheManager.Remove("language-" + viewModel.Code);
                LanguageBE entity;
                entity = Mapper.Map<LanguageViewModel, LanguageBE>(viewModel);
                bool result = languageDAL.AddDictionary(entity);
                return result;
                
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateDictionary(LanguageViewModel viewModel)
        {
            try
            {
                LanguageDAL languageDAL = new LanguageDAL();
                CacheManager.Remove("language-" + viewModel.Code);

                bool result = languageDAL.UpdateDictionary(viewModel);
                return result;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        protected override bool IsValid(LanguageViewModel viewModel)
        {

            if (viewModel.Name != "" &&
                viewModel.Name.Length < 51 &&
                viewModel.Code != "" &&
                viewModel.Code.Length < 6)
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
