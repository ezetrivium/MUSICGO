using BE.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ViewModels;

namespace DAL.Mappers
{
    public class BinnacleDAL : IDAL<BinnacleBE>
    {
        public Guid Add(BinnacleBE entity)
        {
            var dbContext = new DBContext();
            var parameters = Array.Empty<SqlParameter>();

            parameters = new SqlParameter[3];
            if (entity.User == null)
            {
                parameters[0] = dbContext.CreateNullParameters("@UserID");
            }
            else
            {
                parameters[0] = dbContext.CreateParameters("@UserID", entity.User.Id.ToString());
            }

            var guid = Guid.NewGuid();
            parameters[1] = dbContext.CreateParameters("@Description", entity.Description);
            parameters[2] = dbContext.CreateParameters("@binnacleID", guid);

            if (dbContext.Write("AddBinnacle", parameters) > 0)
            {
                return guid;
            }
            return Guid.Empty;
        }


        public IList<BinnacleBE> GetBinnacleWithFilters(DateTime? DateTo, DateTime? DateFrom, string UserName)
        {
            var dbContext = new DBContext();
            var parameters = Array.Empty<SqlParameter>();

            parameters = new SqlParameter[3];

            parameters[0] = dbContext.CreateParameters("@DateFrom", DateFrom.HasValue ? DateFrom.Value : default(DateTime?));
            parameters[1] = dbContext.CreateParameters("@DateTo", DateTo.HasValue ? DateTo.Value : default(DateTime?));
            parameters[2] = dbContext.CreateParameters("@UserName", string.IsNullOrEmpty(UserName) ? null : UserName);


            var binnacleList = new List<BinnacleBE>();
            var dataTable = dbContext.Read("GetBinnacle", parameters);

            UserBE user = null;

            foreach (DataRow row in dataTable.Tables[0].Rows)
            {
                var register = new BinnacleBE
                {
                    Id = Guid.Parse(row["BinnacleID"].ToString()),
                    Description = row["Description"].ToString(),
                    Date = DateTime.Parse(row["Date"].ToString())
                    
                   
                };

                if (row["UserID"] != null)
                {
                    user = new UserBE
                    {
                        Id = Guid.Parse(row["UserID"].ToString()),
                        UserName = row["UserName"].ToString(),
                    };
                }

                register.User = user;

                binnacleList.Add(register);
            }

            return binnacleList;
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<BinnacleBE> Get()
        {
            throw new NotImplementedException();
        }

        public BinnacleBE GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(BinnacleBE entity)
        {
            throw new NotImplementedException();
        }



    }
}
