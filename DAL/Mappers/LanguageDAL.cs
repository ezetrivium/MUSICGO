using BE.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DAL.Mappers
{
    public class LanguageDAL : IDAL<LanguageBE>
    {


        public Guid Add(LanguageBE entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<LanguageBE> Get()
        {
            try
            {

                var dbContext = new DBContext();

                DataSet dataSet;
                List<LanguageBE> languages = new List<LanguageBE>();

                dataSet = dbContext.Read("GetLanguages", null);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        languages.Add(new LanguageBE()
                        {
                            Id = Helper.GetGuidDB(dr["LanguageID"]),
                            Name = Helper.GetStringDB(dr["Name"]),
                            Code = Helper.GetStringDB(dr["Code"])
                        });
                    }

                }

                return languages;

            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }

        public LanguageBE GetById(Guid id)
        {
            var dbContext = new DBContext();
            var dataSet = new DataSet();
            var parameters = new SqlParameter[1];
            parameters[0] = dbContext.CreateParameters("@languageID", id.ToString());

            dataSet = dbContext.Read("GetLanguageById", parameters);
            DataRow dr = dataSet.Tables[0].Rows[0];

            LanguageBE language = new LanguageBE()
            {
                Id = Helper.GetGuidDB(dr["LanguageID"]),
                Code = Helper.GetStringDB(dr["Code"]),
                Name = Helper.GetStringDB(dr["Name"]),
                Dictionary = new Dictionary<string, string>()
            };
            foreach(DataRow drdic in dataSet.Tables[0].Rows)
            {
                language.Dictionary.Add(drdic["Key"].ToString(), drdic["Value"].ToString());
            }


            return language;
        }

        public bool Update(LanguageBE entity)
        {
            throw new NotImplementedException();
        }
    }
}
