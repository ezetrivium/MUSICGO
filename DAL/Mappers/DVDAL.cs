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
    public class DVDAL : IDAL<DVVBE>
    {
        public bool Add(DVVBE entity)
        {
            var dbContext = new DBContext();
            var parameters = new SqlParameter[2];
            parameters[0] = dbContext.CreateParameters("@DVVID", entity.Id.ToString());
            parameters[1] = dbContext.CreateParameters("@TableName", entity.TableName);
            parameters[2] = dbContext.CreateParameters("@DVVHash", entity.DVVHash);

            if(dbContext.Write("SetDVV", parameters) != -1)
            {
                return true;
            }
            return false;
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<DVVBE> Get()
        {
            var dvvList = new List<DVVBE>();
            var dataSet = new DBContext().Read("GetDVV", null);
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                var dvvDb = new DVVBE
                {
                    Id = Guid.Parse(row["DVVID"].ToString()),
                    TableName = row["TableName"].ToString(),
                    DVVHash = row["DVVHash"].ToString()
                };
                dvvList.Add(dvvDb);
            }

            return dvvList;
        }

        public DVVBE GetById(Guid id)
        {
            throw new NotImplementedException();
        }


        public bool Update(DVVBE entity)
        {
            var dbContext = new DBContext();
            var parameters = new SqlParameter[2];
            parameters[0] = dbContext.CreateParameters("@TableName", entity.TableName);
            parameters[1] = dbContext.CreateParameters("@DVVHash", entity.DVVHash);

            if (dbContext.Write("SetDVV", parameters) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
