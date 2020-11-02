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
    public class ContractDAL : IDAL<ContractBE>
    {
        public Guid Add(ContractBE entity)
        {
            var dbContext = new DBContext();
            try
            {

                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();


                Guid contractID = Guid.NewGuid();
               
                parameters = new SqlParameter[3];

                parameters[0] = dbContext.CreateParameters("@serviceID", entity.Service.Id );
                parameters[1] = dbContext.CreateParameters("@userID", entity.User.Id);
                parameters[2] = dbContext.CreateParameters("@contractID", contractID);


                if (dbContext.Write("CreateContract", parameters) > 0)
                {
                    return contractID;
                }
                else
                {
                    return Guid.Empty;
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception(Messages.Generic_Error);
            }
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<ContractBE> Get()
        {
            throw new NotImplementedException();
        }

        public ContractBE GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(ContractBE entity)
        {
            throw new NotImplementedException();
        }
    }
}
