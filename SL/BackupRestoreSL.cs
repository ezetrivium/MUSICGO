using BE.Entities;
using DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Exceptions;

namespace SL
{
    public class BackupRestoreSL
    {

        private BackupDAL Dal;
        public BackupRestoreSL()
        {
            this.Dal = new BackupDAL();
        }


        public bool CreateBackup()
        {
            try
            {
                DVVerifier dvvv = new DVVerifier();

                var result = dvvv.DVVerify();

                if (result)
                {
                    BackupDataBE backup = new BackupDataBE()
                    {
                        Date = DateTime.Now,
                        Path = GlobalValues.BackupRepo
                    };

                    if (Dal.Set(backup))
                    {
                        return true;
                    }
                    else
                    {
                        throw new BusinessException(Messages.ErrorBackup);
                    }
                }
                else
                {
                    throw new BusinessException(Messages.ErrorBackupVerify);
                }
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

        public IList<BackupDataBE> GetBackups()
        {
            return this.Dal.Get();
        }


        public bool Restore(BackupDataBE backup)
        {
            try
            {

                if(Dal.Restore(backup))
                {
                    return true;
                }
                else
                {
                    throw new BusinessException(Messages.ErrorRestore);
                }

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
    }

}
