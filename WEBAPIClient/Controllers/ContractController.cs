using BLL.BLLs;
using Institucional.WEBAPIClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Utilities;
using Utilities.Exceptions;
using ViewModels.ViewModels;

namespace WEBAPIClient.Controllers
{
    public class ContractController : BaseApiController
    {
        #region Fields
        private readonly ContractBLL contractBLL;
        #endregion Fields

        #region Constructors
        public ContractController()
        {
            this.contractBLL = new ContractBLL();
        }
        #endregion Constructors



        [HttpPost]

        [Route("api/contract/contractservice")]
        public IHttpActionResult Post(UserViewModel userViewModel)
        {
            try
            {
                if (contractBLL.Add(userViewModel.Contract) == Guid.Empty)
                {
                    throw new BusinessException(Messages.ErrorContract); 
                }
                
                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, Messages.SuccessfulContract);

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