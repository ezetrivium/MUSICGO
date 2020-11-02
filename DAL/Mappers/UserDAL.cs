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
using Utilities.Exceptions;

namespace DAL.Mappers
{
    public class UserDAL : IDAL<UserBE>, IDV<UserBE>
    {
        public Guid Add(UserBE entity)
        {
            var dbContext = new DBContext();
            try
            {
                
                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();


                var userguid = Guid.NewGuid();
                parameters = new SqlParameter[11];

                parameters[0] = dbContext.CreateParameters("@imgKey", !string.IsNullOrEmpty(entity.ImgKey) ? entity.ImgKey : null);
                parameters[1] = dbContext.CreateParameters("@userName", entity.UserName);
                parameters[2] = dbContext.CreateParameters("@lastName", entity.LastName);
                parameters[3] = dbContext.CreateParameters("@name", entity.Name);
                parameters[4] = dbContext.CreateParameters("@languageID", entity.Language.Id);
                parameters[5] = dbContext.CreateParameters("@email", !string.IsNullOrEmpty(entity.Email) ? entity.Email : null);
                parameters[6] = dbContext.CreateParameters("@password", entity.Password);
                parameters[8] = dbContext.CreateParameters("@artistName", !string.IsNullOrEmpty(entity.ArtistName) ? entity.ArtistName : null);
                parameters[7] = dbContext.CreateParameters("@serviceID", entity.Contract.Service.Id == Guid.Empty ? Guid.Empty : entity.Contract.Service.Id);
                parameters[9] = dbContext.CreateParameters("@userid", userguid);
                parameters[10] = dbContext.CreateParameters("@contractID", entity.Contract.Service.Id == Guid.Empty ? Guid.Empty : Guid.NewGuid());

                dbContext.BeginTran();
                if (dbContext.Write("AddUser", parameters) > 0)
                {
                    entity.Id = userguid;
                    PermissionDAL per = new PermissionDAL();
                    bool resultper = per.SetUserPermission(entity,dbContext);

                    if (resultper)
                    {
                        dbContext.CommitTran();
                        return userguid;
                    }
                    
                }
                dbContext.RollBackTran();
                return Guid.Empty;
            }
            catch (Exception ex)
            {
                dbContext.RollBackTran();
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

                parameters[0] = dbContext.CreateParameters("@userID", id);

                if (dbContext.Write("DeleteUser", parameters) > 0)
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

        public UserBE GetDVHEntity(Guid id)
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();
                parameters = new SqlParameter[1];

                parameters[0] = dbContext.CreateParameters("@userID", id);


                UserBE user = null;
                dataSet = dbContext.Read("GetUserToCalculateDVH", parameters);

                if (dataSet.Tables[0].Rows.Count > 0)
                {

                    var dr = dataSet.Tables[0].Rows[0];
                    user = new UserBE()
                    {
                        Id = Helper.GetGuidDB(dr["UserID"]),
                        Name = Helper.GetStringDB(dr["Name"]),
                        UserName = Helper.GetStringDB(dr["UserName"]),
                        LastName = Helper.GetStringDB(dr["LastName"]),
                        Language = new LanguageBE()
                        {
                            Id = Helper.GetGuidDB(dr["LanguageID"])
                        },
                        Playbacks = Helper.GetIntDB(dr["Playbacks"]),
                        Password = Helper.GetStringDB(dr["Password"]),
                        Email = Helper.GetStringDB(dr["Email"]),
                        Blocked = Helper.GetBoolDB(dr["Blocked"]),
                        ImgKey = Helper.GetStringDB(dr["ImgKey"]),
                        DVH = Helper.GetStringDB(dr["DVH"]),
                        ArtistName = Helper.GetStringDB(dr["ArtistName"]),
                        BlockedDateTime = Helper.GetDateTimeDB(dr["BlockedDateTime"]),
                        Contract = new ContractBE()
                        {
                            Id = Helper.GetGuidDB(dr["ContractID"])
                        }
                    };                  

                }


                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }

        public IList<UserBE> GetDVHEntities() 
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                List<UserBE> Users = new List<UserBE>();

                dataSet = dbContext.Read("GetUserDVH", null);

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
                            Language = new LanguageBE() 
                            { 
                                Id = Helper.GetGuidDB(dr["LanguageID"])
                            },
                            Playbacks = Helper.GetIntDB(dr["Playbacks"]),
                            Password = Helper.GetStringDB(dr["Password"]),
                            Email = Helper.GetStringDB(dr["Email"]),
                            Blocked = Helper.GetBoolDB(dr["Blocked"]),
                            ImgKey = Helper.GetStringDB(dr["ImgKey"]),
                            DVH = Helper.GetStringDB(dr["DVH"]),
                            ArtistName = Helper.GetStringDB(dr["ArtistName"]),
                            BlockedDateTime = Helper.GetDateTimeDB(dr["BlockedDateTime"]),
                            Contract = new ContractBE()
                            {
                                Id = Helper.GetGuidDB(dr["ContractID"])
                            }
                        });
                    }

                }


                return Users;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
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
                            ImgKey = Helper.GetStringDB(dr["ImgKey"]),
                            DVH = Helper.GetStringDB(dr["DVH"]),
                            ArtistName = Helper.GetStringDB(dr["ArtistName"]),
                            Contract = new ContractBE()
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
            try
            {
                UserBE user = new UserBE();
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                var parameters = new SqlParameter[1];
                parameters[0] = dbContext.CreateParameters("@UserID", id);

                dataSet = dbContext.Read("GetUserByID", parameters);


                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dataSet.Tables[0].Rows[0];
                    user.Id = Helper.GetGuidDB(dr["UserID"]);
                    user.Name = Helper.GetStringDB(dr["Name"]);
                    user.UserName = Helper.GetStringDB(dr["UserName"]);
                    user.LastName = Helper.GetStringDB(dr["LastName"]);
                    user.ArtistName = Helper.GetStringDB(dr["ArtistName"]);
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
                    if (Helper.GetGuidDB(dr["ContractID"]) != Guid.Empty)
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
                                Price = Helper.GetDoubleDB(dr["Price"]),
                                Code = Helper.GetStringDB(dr["ServiceCode"])
                            }
                        };
                    }

                }
                else
                {
                    throw new BusinessException(Messages.InvalidDataUserID);
                }

                return user;
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }

        public bool Update(UserBE entity)
        {
            var dbContext = new DBContext();
            try
            {
               
                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();
                PermissionDAL permissionDAL = new PermissionDAL();

                parameters = new SqlParameter[11];

                parameters[0] = dbContext.CreateParameters("@userid", entity.Id);
                parameters[1] = dbContext.CreateParameters("@userName", entity.UserName);
                parameters[2] = dbContext.CreateParameters("@lastName", entity.LastName);
                parameters[3] = dbContext.CreateParameters("@name", entity.Name);
                parameters[4] = dbContext.CreateParameters("@languageID", entity.Language.Id);
                parameters[5] = dbContext.CreateParameters("@email", !string.IsNullOrEmpty(entity.Email) ? entity.Email : null);
                parameters[6] = dbContext.CreateParameters("@password", entity.Password);
                
                parameters[9] = dbContext.CreateParameters("@artistName", !string.IsNullOrEmpty(entity.ArtistName) ? entity.ArtistName : null);
                parameters[7] = dbContext.CreateParameters("@contractID", entity.Contract == null ? Guid.Empty : Guid.NewGuid());
                parameters[8] = dbContext.CreateParameters("@imgKey", !string.IsNullOrEmpty(entity.ImgKey) ? entity.ImgKey : null);
                parameters[10] = dbContext.CreateParameters("@serviceID", entity.Contract == null  ? Guid.Empty : entity.Contract.Service.Id);

                dbContext.BeginTran();
                if (dbContext.Write("UpdateUser", parameters) > 0)
                {


                    permissionDAL.DeleteUserPermission(entity, dbContext);
                    
                    permissionDAL.SetUserPermission(entity, dbContext);
                    
               
                    dbContext.CommitTran();
                    return true;
                    

                }
                dbContext.RollBackTran();
                return false;
            }
            catch(Exception ex)
            {
                dbContext.RollBackTran();
                throw new Exception(Messages.Generic_Error);
            }
            
        }
        public bool Block(UserBE user)
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();
                

                parameters = new SqlParameter[1];

                parameters[0] = dbContext.CreateParameters("@userID", user.Id);


                if (dbContext.Write("BlockUser", parameters) > 0)
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
        public UserBE GetUserByUserName(UserBE user)
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                var parameters = new SqlParameter[1];
                parameters[0] = dbContext.CreateParameters("@UserName", user.UserName);

                dataSet = dbContext.Read("GetUserByUserName", parameters);


                var userCheck = new UserBE();

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dataSet.Tables[0].Rows[0];
                    userCheck.Id = Helper.GetGuidDB(dr["UserID"]);
                    userCheck.Name = Helper.GetStringDB(dr["Name"]);
                    userCheck.UserName = Helper.GetStringDB(dr["UserName"]);
                    userCheck.LastName = Helper.GetStringDB(dr["LastName"]);
                    userCheck.ArtistName = Helper.GetStringDB(dr["ArtistName"]);
                    userCheck.Language = new LanguageBE()
                    {
                        Id = Helper.GetGuidDB(dr["LanguageID"]),
                        Name = Helper.GetStringDB(dr["LanguageName"]),
                        Code = Helper.GetStringDB(dr["Code"]),
                    };
                    userCheck.Playbacks = Helper.GetIntDB(dr["Playbacks"]);
                    userCheck.Password = Helper.GetStringDB(dr["Password"]);
                    userCheck.Email = Helper.GetStringDB(dr["Email"]);
                    userCheck.Blocked = Helper.ParseBoolDB(dr["Blocked"]);
                    userCheck.ImgKey = Helper.GetStringDB(dr["ImgKey"]);
                    if(Helper.GetGuidDB(dr["ContractID"]) != Guid.Empty)
                    {
                        userCheck.Contract = new ContractBE()
                        {
                            Id = Helper.GetGuidDB(dr["ContractID"]),
                            ExpirationDate = Helper.GetDateTimeDB(dr["ExpirationDate"]),
                            HireDate = Helper.GetDateTimeDB(dr["HireDate"]),
                            Service = new ServiceBE()
                            {
                                Id = Helper.GetGuidDB(dr["ServiceID"]),
                                Description = Helper.GetStringDB(dr["Description"]),
                                Name = Helper.GetStringDB(dr["ServiceName"]),
                                Price = Helper.GetDoubleDB(dr["Price"]),
                                Code = Helper.GetStringDB(dr["ServiceCode"])
                            }
                        };
                    }
                    
                }

                return userCheck;
            }
            catch(Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
                    
        }

        public bool SetDVH(UserBE entity)
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();


                var userguid = Guid.NewGuid();
                parameters = new SqlParameter[2];

                parameters[0] = dbContext.CreateParameters("@userID", entity.Id);
                parameters[1] = dbContext.CreateParameters("@dvh", entity.DVH);


                if (dbContext.Write("SetUserDVH", parameters) > 0)
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
