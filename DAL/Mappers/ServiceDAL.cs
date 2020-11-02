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
    public class ServiceDAL : IDAL<ServiceBE>
    {
        public Guid Add(ServiceBE entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<ServiceBE> Get()
        {
            try
            {

                var dbContext = new DBContext();

                DataSet dataSet;
                List<ServiceBE> services = new List<ServiceBE>();

                dataSet = dbContext.Read("GetServices", null);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        services.Add(new ServiceBE()
                        {
                            Id = Helper.GetGuidDB(dr["ServiceID"]),
                            Name = Helper.GetStringDB(dr["Name"]),
                            Price = Helper.GetDoubleDB(dr["Price"]),
                            Description = Helper.GetStringDB(dr["Description"]),
                            Code = Helper.GetStringDB(dr["Code"])
                        });
                    }

                }

                return services;

            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }

        }


        public ServiceBE GetById(Guid id)
        {
            try
            {
                ServiceBE service = new ServiceBE();
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                var parameters = new SqlParameter[1];
                parameters[0] = dbContext.CreateParameters("@serviceID", id);

                dataSet = dbContext.Read("GetServiceByID", parameters);


                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dataSet.Tables[0].Rows[0];
                    service.Id = Helper.GetGuidDB(dr["ServiceID"]);
                    service.Name = Helper.GetStringDB(dr["Name"]);
                    service.Description = Helper.GetStringDB(dr["Description"]);
                    service.Code = Helper.GetStringDB(dr["Code"]);
                    service.Price = Helper.GetDoubleDB(dr["Price"]);


                }
                else
                {
                    throw new Exception(Messages.Generic_Error);
                }

                return service;
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

        public bool Update(ServiceBE entity)
        {
            throw new NotImplementedException();
        }
    }
}
