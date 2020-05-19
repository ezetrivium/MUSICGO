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
    public class UserDAL : IDAL<UserBE>
    {
        public bool Add(UserBE entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<UserBE> Get()
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                List<UserBE> Users = new List<UserBE>();

                dataSet = dbContext.Read("GetUsers", null);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        Users.Add(new UserBE()
                        {
                            Id = Helper.GetGuidDB(dr["UserID"]),
                            Name = Helper.GetStringDB(dr["Name"]),
                            UserName = Helper.GetStringDB(dr["UserName"]),
                            LastName = Helper.GetStringDB(dr["LastName"]),
                            Language = new LanguageBE(),
                            Playbacks = Helper.GetIntDB(dr["Playbacks"]),
                            Password = Helper.GetStringDB(dr["Password"]),
                            Email = Helper.GetStringDB(dr["Email"]),
                            Blocked = Helper.GetBoolDB(dr["Blocked"]),
                            ImgKey = Helper.GetStringDB(dr["ImgKey"])
                        });
                    }

                }


                return Users;
            }
            catch(Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
            
        }

        public UserBE GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(UserBE entity)
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();

                parameters = new SqlParameter[12];

                parameters[0] = dbContext.CreateParameters("@userID", entity.Id);
                parameters[1] = dbContext.CreateParameters("@userName", entity.UserName);
                parameters[2] = dbContext.CreateParameters("@lastName", entity.LastName);
                parameters[3] = dbContext.CreateParameters("@name", entity.Name);
                parameters[4] = dbContext.CreateParameters("@languageID", entity.Language.Id);
                parameters[5] = dbContext.CreateParameters("@email", !string.IsNullOrEmpty(entity.Email) ? entity.Email : null);
                parameters[6] = dbContext.CreateParameters("@password", entity.Password);
                parameters[7] = dbContext.CreateParameters("@playbacks", entity.Playbacks);
                parameters[8] = dbContext.CreateParameters("@blocked", entity.Blocked);
                parameters[9] = dbContext.CreateParameters("@artistName", !string.IsNullOrEmpty(entity.ArtistName) ? entity.ArtistName : null);
                parameters[10] = dbContext.CreateParameters("@contractID", entity.Contract == null ? null : entity.Contract.Id.ToString());
                parameters[11] = dbContext.CreateParameters("@imgKey", !string.IsNullOrEmpty(entity.ImgKey) ? entity.ImgKey : null);

                if (dbContext.Write("UpdateUser", parameters) > 0)
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
            
        }

        public UserBE GetUserByUserName(UserBE user)
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                var parameters = new SqlParameter[1];
                parameters[0] = dbContext.CreateParameters("@UserName", user.UserName);

                dataSet = dbContext.Read("GetUserByUserName", parameters);


                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dataSet.Tables[0].Rows[0];
                    user.Id = Helper.GetGuidDB(dr["UserID"]);
                    user.Name = Helper.GetStringDB(dr["Name"]);
                    user.UserName = Helper.GetStringDB(dr["UserName"]);
                    user.LastName = Helper.GetStringDB(dr["LastName"]);
                    user.Language = new LanguageBE()
                    {
                        Id = Helper.GetGuidDB(dr["LanguageID"]),
                        Name = Helper.GetStringDB(dr["LanguageName"]),
                        Code = Helper.GetStringDB(dr["Code"]),
                    };
                    user.Playbacks = Helper.GetIntDB(dr["Playbacks"]);
                    user.Password = Helper.GetStringDB(dr["Password"]);
                    user.Email = Helper.GetStringDB(dr["Email"]);
                    user.Blocked = Helper.ParseBoolDB(dr["Blocked"]);
                    user.ImgKey = Helper.GetStringDB(dr["ImgKey"]);
                    if(Helper.GetGuidDB(dr["ContractID"]) != Guid.Empty)
                    {
                        user.Contract = new ContractBE()
                        {
                            Id = Helper.GetGuidDB(dr["ContractID"]),
                            ExpirationDate = Helper.GetDateTimeDB(dr["ExpirationDate"]),
                            HireDate = Helper.GetDateTimeDB(dr["HireDate"]),
                            Service = new ServiceBE()
                            {
                                Id = Helper.GetGuidDB(dr["ServiceID"]),
                                Description = Helper.GetStringDB(dr["Description"]),
                                Name = Helper.GetStringDB(dr["ServiceName"]),
                                Price = Helper.GetDoubleDB(dr["Price"])
                            }
                        };
                    }
                    
                }

                return user;
            }
            catch(Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
                    
        }


    }
}
