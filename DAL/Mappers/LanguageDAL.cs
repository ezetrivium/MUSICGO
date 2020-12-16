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
using ViewModels.ViewModels;

namespace DAL.Mappers
{
    public class LanguageDAL : IDAL<LanguageBE>
    {


        public Guid Add(LanguageBE entity)
        {
            var dbContext = new DBContext();
            try
            {

                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();


                var languageguid = Guid.NewGuid();
                parameters = new SqlParameter[3];

                parameters[0] = dbContext.CreateParameters("@languageID", languageguid);
                parameters[1] = dbContext.CreateParameters("@name", entity.Name);
                parameters[2] = dbContext.CreateParameters("@code", entity.Code);
                

               
                if (dbContext.Write("AddLanguage", parameters) > 0)
                {
                    return languageguid;

                }
                
                return Guid.Empty;
            }
            catch (Exception ex)
            {
              
                throw new Exception(Messages.Generic_Error);
            }
        }


        public bool AddDictionary(LanguageBE entity)
        {
            var dbContext = new DBContext();
            try
            {

                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();

                var result = false;
   
                parameters = new SqlParameter[3];


                

                //foreach (var dic in entity.Dictionary)
                //{
                    parameters[0] = dbContext.CreateParameters("@languageID", entity.Id);
                    parameters[1] = dbContext.CreateParameters("@key", entity.Dictionary.First().Key);
                    parameters[2] = dbContext.CreateParameters("@value", entity.Dictionary.First().Value);



                    result = dbContext.Write("AddDictionary", parameters) > 0 ? true : false;

                    //if(!result)
                    //{
                        
                    //    break;
                    //}

                //}

           

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }




        public bool Delete(Guid id)
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();

                parameters = new SqlParameter[1];

                parameters[0] = dbContext.CreateParameters("@languageID", id);

                if (dbContext.Write("DeleteLanguage", parameters) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
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


        public Dictionary<Guid, string> GetDictionaryKeysAll()
        {
            try
            {

                var dbContext = new DBContext();
                Dictionary<Guid, string> dic = new Dictionary<Guid, string>();
                DataSet dataSet;
                

                dataSet = dbContext.Read("GetDictionaryKeysAll", null);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {

                        dic.Add(Helper.GetGuidDB(dr["ID"]), dr["Key"].ToString());


                    }

                }

                return dic;

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

            LanguageBE language = null;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dataSet.Tables[0].Rows[0];

                language = new LanguageBE()
                {
                    Id = Helper.GetGuidDB(dr["LanguageID"]),
                    Code = Helper.GetStringDB(dr["Code"]),
                    Name = Helper.GetStringDB(dr["Name"]),
                    Dictionary = new Dictionary<string, string>()
                };
           
                foreach (DataRow drdic in dataSet.Tables[0].Rows)
                {
                    language.Dictionary.Add(drdic["Key"].ToString(), drdic["Value"].ToString());
                }
            }
            


            return language;
        }


        public Dictionary<Guid,string> GetDictionaryKeys(Guid languageID)
        {
            try
            {

                var dbContext = new DBContext();

                DataSet dataSet;
                List<LanguageBE> languages = new List<LanguageBE>();

                Dictionary<Guid, string> dic = new Dictionary<Guid, string>();
                var parameters = new SqlParameter[1];

                parameters[0] = dbContext.CreateParameters("@languageID", languageID);

                dataSet = dbContext.Read("GetDictionaryKeys", parameters);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        
                        dic.Add(Helper.GetGuidDB(dr["ID"]), dr["Key"].ToString());
                        

                    }

                }

                return dic;

            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }



        public LanguageBE GetByCode(string code)
        {
            var dbContext = new DBContext();
            var dataSet = new DataSet();
            var parameters = new SqlParameter[1];
            parameters[0] = dbContext.CreateParameters("@code", code);

            dataSet = dbContext.Read("GetLanguageByCode", parameters);

            LanguageBE language = null;

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dataSet.Tables[0].Rows[0];

                language = new LanguageBE()
                {
                    Id = Helper.GetGuidDB(dr["LanguageID"]),
                    Code = Helper.GetStringDB(dr["Code"]),
                    Name = Helper.GetStringDB(dr["Name"]),
                    Dictionary = new Dictionary<string, string>()
                };
                foreach (DataRow drdic in dataSet.Tables[0].Rows)
                {
                    language.Dictionary.Add(drdic["Key"].ToString(), drdic["Value"].ToString());
                }
            }
           


            return language;
        }

        public bool Update(LanguageBE entity)
        {
            var dbContext = new DBContext();
            try
            {

                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();
                PermissionDAL permissionDAL = new PermissionDAL();

                parameters = new SqlParameter[3];

                parameters[0] = dbContext.CreateParameters("@languageID", entity.Id);
                parameters[1] = dbContext.CreateParameters("@name", entity.Name);
                parameters[2] = dbContext.CreateParameters("@code", entity.Code);



                if (dbContext.Write("UpdateLanguage", parameters) > 0)
                {

                    return true;

                }
               
                return false;
            }
            catch (Exception ex)
            {
               
                throw new Exception(Messages.Generic_Error);
            }
        }



        public bool UpdateDictionary(LanguageViewModel languageViewModel)
        {
            var dbContext = new DBContext();
            try
            {

                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();
                PermissionDAL permissionDAL = new PermissionDAL();

                parameters = new SqlParameter[3];

                parameters[0] = dbContext.CreateParameters("@languageID", languageViewModel.Id);
                parameters[1] = dbContext.CreateParameters("@key", languageViewModel.Dictionary.First().Key);
                parameters[2] = dbContext.CreateParameters("@value", languageViewModel.Dictionary.First().Value);





                if (dbContext.Write("UpdateDictionary", parameters) > 0)
                {

                    return true;

                }

                return false;
            }
            catch (Exception ex)
            {
               
                throw new Exception(Messages.Generic_Error);
            }
        }
    }
}
