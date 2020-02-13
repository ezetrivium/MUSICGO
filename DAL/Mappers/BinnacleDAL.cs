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
        public bool Add(BinnacleBE entity)
        {
            var dbContext = new DBContext();
            var parameters = Array.Empty<SqlParameter>();

            parameters = new SqlParameter[2];
            if (entity.User == null)
            {
                parameters[0] = dbContext.CreateNullParameters("@UserID");
            }
            else
            {
                parameters[0] = dbContext.CreateParameters("@UserID", entity.User.Id.ToString());
            }
            
            parameters[1] = dbContext.CreateParameters("@Description", entity.Description);

            if(dbContext.Write("AddBinnacle", parameters) > 0)
            {
                return true;
            }
            return false;
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

            foreach (DataRow row in dataTable.Tables[0].Rows)
            {
                var register = new BinnacleBE
                {
                    Id = Guid.Parse(row["BinnacleID"].ToString()),
                    Description = row["Description"].ToString(),
                    Date = DateTime.Parse(row["Date"].ToString()),
                    User = new UserBE
                    {
                        Id = Guid.Parse(row["UserID"].ToString()),
                        UserName = row["UserName"].ToString(),
                    }
                };

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
