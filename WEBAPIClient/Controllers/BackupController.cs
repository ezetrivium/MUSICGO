using BE.Entities;
using Institucional.WEBAPIClient;
using SL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Utilities;
using Utilities.Exceptions;

namespace WEBAPIClient.Controllers
{
    public class BackupController : BaseApiController
    {
        #region Fields
        private readonly BackupRestoreSL backupRestoreSL;
        #endregion Fields

        #region Constructors
        public BackupController()
        {
            this.backupRestoreSL = new BackupRestoreSL();
        }
        #endregion Constructors



        [HttpGet]
        [Authorize(Roles = "Backup")]
        [Route("api/backup/get")]
        public IEnumerable<BackupDataBE> Get()
        {
            return this.backupRestoreSL.GetBackups();
        }



        [HttpPost]
        [Authorize(Roles = "Backup")]
        [Route("api/backup/set")]
        public IHttpActionResult Post()
        {
            try
            {


                var result = this.backupRestoreSL.CreateBackup();
                if (!result)
                {
                    throw new BusinessException(Messages.ErrorSetPermission);
                }
                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, Messages.SuccessfulBackup);

                return this.ResponseMessage(response);
            }
            catch (BusinessException ex)
            {
                var response = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
                return this.ResponseMessage(response);
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, Messages.Generic_Error);
                return this.ResponseMessage(response);
            }

        }



        [HttpPost]
        [Authorize(Roles = "Restore")]
        [Route("api/backup/restore")]
        public IHttpActionResult Restore(BackupDataBE backupDataBE)
        {
            try
            {


                var result = this.backupRestoreSL.Restore(backupDataBE);
                if (!result)
                {
                    throw new BusinessException(Messages.ErrorSetPermission);
                }
                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, Messages.SuccessfulRestore);

                return this.ResponseMessage(response);
            }
            catch (BusinessException ex)
            {
                var response = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
                return this.ResponseMessage(response);
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, Messages.Generic_Error);
                return this.ResponseMessage(response);
            }

        }

    }
}