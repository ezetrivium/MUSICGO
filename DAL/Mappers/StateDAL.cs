using BE.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DAL.Mappers
{
    public class StateDAL : IDAL<StateBE>
    {
        public Guid Add(StateBE entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<StateBE> Get()
        {
            try
            {

                var dbContext = new DBContext();

                DataSet dataSet;
                List<StateBE> states = new List<StateBE>();

                dataSet = dbContext.Read("GetStates", null);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        states.Add(new StateBE()
                        {
                            Id = Helper.GetGuidDB(dr["StateID"]),
                            Name = Helper.GetStringDB(dr["Name"])
                        });
                    }

                }

                return states;

            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }

        public StateBE GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(StateBE entity)
        {
            throw new NotImplementedException();
        }
    }
}
