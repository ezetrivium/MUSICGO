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

        public IEnumerable<UserBE> Get()
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                List<UserBE> Users = new List<UserBE>();

                dataSet = dbContext.Read("GetUsers", null);


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
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                var parameters = new SqlParameter[1];
                parameters[0] = dbContext.CreateParameters("@UserName", user.UserName);

                dataSet = dbContext.Read("GetUserByUserName", parameters);
                DataRow dr = dataSet.Tables[0].Rows[0];

                user.Id = Guid.Parse(dr["UserID"].ToString());
                user.Name = dr["Name"].ToString();
                user.UserName = dr["UserName"].ToString();
                user.LastName = dr["LastName"].ToString();
                user.Language = new LanguageBE()
                {
                    Id = Guid.Parse(dr["LanguageID"].ToString()),
                    Name = dr["Name"].ToString(),
                    Code = dr["Code"].ToString(),
                };
                user.Playbacks = int.Parse(dr["Playbacks"].ToString());
                user.Password = dr["Password"].ToString();
                user.Email = dr["Email"].ToString();
                user.Blocked = Convert.ToBoolean(dr["Blocked"]);

                return user;
            }
            catch(Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
            
        }


    }
}
