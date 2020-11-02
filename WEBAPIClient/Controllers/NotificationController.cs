using BLL.BLLs;
using Institucional.WEBAPIClient;
using MercadoPago.DataStructures.Customer;
using MercadoPago.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Utilities;
using Utilities.Exceptions;
using ViewModels.ViewModels;

namespace WEBAPIClient.Controllers
{
    public class NotificationController : BaseApiController
    {
       

        [HttpGet]
        [Route("api/mercadopago/notification")]

        public IHttpActionResult Get(string externalReference)
        {
            try
            {
                ServiceBLL serviceBLL = new ServiceBLL();
                UserBLL userBLL = new UserBLL();
                var contractBLL = new ContractBLL();
                if (string.IsNullOrEmpty(externalReference))
                    throw new Exception();

                //var paymentInfo = await Task.Run(() => Payment.FindById(long.Parse(id)));

                //var extRef = paymentInfo.ExternalReference;
                //var status = paymentInfo.Status;
                var user = userBLL.GetById(Guid.Parse(externalReference.Split('/')[0]));

                if (user.Contract == null && user.Permissions.Where(p => p.Name != "Login").ToList().Count > 0)
                {
                    throw new BusinessException(Messages.ErrorContractUser);
                }

                if (user.Contract != null && user.Contract.Service.Id == Guid.Parse(externalReference.Split('/')[1]))
                {
                    if (user.Contract.ExpirationDate > DateTime.Now)
                    {
                        throw new BusinessException(Messages.ErrorContractExists);
                    }
                }


                ContractViewModel cvm = new ContractViewModel()
                {
                    User = user,
                    
                    Service = serviceBLL.GetById(Guid.Parse(externalReference.Split('/')[1]))

                };

                user.Contract = cvm;


                if (!userBLL.Update(user))
                {
                    throw new BusinessException(Messages.ErrorContract);
                }


                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, Messages.SuccessfulContract);

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