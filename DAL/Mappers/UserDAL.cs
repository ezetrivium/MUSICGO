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
                            Id = Guid.Parse(dr["UserID"].ToString()),
                            Name = dr["Name"].ToString(),
                            UserName = dr["UserName"].ToString(),
                            LastName = dr["LastName"].ToString(),
                            Language = new LanguageBE(),
                            Playbacks = int.Parse(dr["Playbacks"].ToString()),
                            Password = dr["Password"].ToString(),
                            Email = dr["Email"].ToString(),
                            Blocked = Convert.ToBoolean(dr["Blocked"]),
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

        public bool Update(UserBE viewModel)
        {
            throw new NotImplementedException();
        }

        public UserBE GetUserByUserName(UserBE user)
        {
            
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                var parameters = new SqlParameter[1];
                parameters[0] = dbContext.CreateParameters("@UserName", user.UserName);

                dataSet = dbContext.Read("GetUserByUserName", parameters);
                

                if(dataSet.Tables[0].Rows.Count > 0)
                {
                DataRow dr = dataSet.Tables[0].Rows[0];
                user.Id = Guid.Parse(Helper.GetStringDB(dr["UserID"].ToString())); 
                user.Name = Helper.GetStringDB(dr["Name"].ToString());
                user.UserName = Helper.GetStringDB(dr["UserName"].ToString());
                user.LastName = Helper.GetStringDB(dr["LastName"].ToString());
                user.Language = new LanguageBE()
                {
                    Id = Guid.Parse(Helper.GetStringDB(dr["LanguageID"].ToString())),
                    Name = Helper.GetStringDB(dr["LanguageName"].ToString()),
                    Code = Helper.GetStringDB(dr["Code"].ToString()),
                };
                user.Playbacks = Helper.GetIntDB(int.Parse(dr["Playbacks"].ToString()));
                user.Password = Helper.GetStringDB(dr["Password"].ToString());
                user.Email = Helper.GetStringDB(dr["Email"].ToString());
                user.Blocked = Helper.ParseBoolDB(Convert.ToBoolean(dr["Blocked"]));
                }
                

                return user;           
        }


    }
}
