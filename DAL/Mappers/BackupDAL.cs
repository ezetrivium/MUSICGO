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
    public class BackupDAL : IDAL<BackupDataBE>
    {
        public bool Set(BackupDataBE entity)
        {
            var dbContext = new DBContext();
            try
            {

                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();


                var languageguid = Guid.NewGuid();
                parameters = new SqlParameter[4];

                Guid guid = Guid.NewGuid();

                parameters[0] = dbContext.CreateParameters("@file", entity.Path + "\\" + guid + ".Bak");
                parameters[1] = dbContext.CreateParameters("@date", entity.Date);
                parameters[2] = dbContext.CreateParameters("@path", entity.Path);
                parameters[3] = dbContext.CreateParameters("@id", guid);





                return dbContext.MakeBackup(parameters) > 0 ? true : false;
                
            }
            catch (Exception ex)
            {
               
                throw new Exception(Messages.Generic_Error);
            }
        }

        public bool Restore(BackupDataBE entity)
        {
            var dbContext = new DBContext();
            try
            {

                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();


                var languageguid = Guid.NewGuid();
                parameters = new SqlParameter[1];

                parameters[0] = dbContext.CreateParameters("@file", entity.Path + "//" + entity.Id + ".Bak");





                return dbContext.MakeRestore(parameters) == -1 ? true : false;

            }
            catch (Exception ex)
            {

                throw new Exception(Messages.Generic_Error);
            }
        }



        public IList<BackupDataBE> Get()
        {
            try
            {

                var dbContext = new DBContext();

                DataSet dataSet;
                List<BackupDataBE> backups = new List<BackupDataBE>();

                dataSet = dbContext.Read("GetBackups", null);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        backups.Add(new BackupDataBE()
                        {
                            Id = Helper.GetGuidDB(dr["ID"]),
                            Date = Helper.GetDateTimeDB(dr["Date"]),
                            Path = Helper.GetStringDB(dr["Path"])
                        });
                    }

                }

                return backups;

            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }


        public Guid Add(BackupDataBE entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }


        public BackupDataBE GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(BackupDataBE entity)
        {
            throw new NotImplementedException();
        }
    }
}
