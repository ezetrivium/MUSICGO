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
    public class CategoryDAL : IDAL<CategoryBE>
    {
        public Guid Add(CategoryBE entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<CategoryBE> Get()
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                List<CategoryBE> categories = new List<CategoryBE>();

                dataSet = dbContext.Read("GetCategories", null);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        categories.Add(new CategoryBE()
                        {
                            Id = Helper.GetGuidDB(dr["CategoryID"]),
                            Name = Helper.GetStringDB(dr["Name"]),

                        });
                    }

                }


                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }

        public CategoryBE GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(CategoryBE entity)
        {
            throw new NotImplementedException();
        }
    }
}
