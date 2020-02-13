using BE.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public class LanguageDAL : IDAL<LanguageBE>
    {


        public bool Add(LanguageBE entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<LanguageBE> Get()
        {
            throw new NotImplementedException();
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
                Id = Guid.Parse(dr["LanguageID"].ToString()),
                Code = dr["Code"].ToString(),
                Name = dr["Name"].ToString(),
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
