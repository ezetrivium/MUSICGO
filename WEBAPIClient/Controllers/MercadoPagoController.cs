using BLL.BLLs;
using Institucional.WEBAPIClient;
using MercadoPago;
using MercadoPago.Common;
using MercadoPago.DataStructures.Preference;
using MercadoPago.Resources;
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
    public class MercadoPagoController : BaseApiController
    {

        

        public MercadoPagoController()
        {
           
        }


        [HttpPost]
        
        [Route("api/mercadopago/preference")]
        public IHttpActionResult Post(UserViewModel userViewModel)
        {
            try
            {

                UserBLL userBLL = new UserBLL();

                

                if(userViewModel.Id == Guid.Empty || userViewModel.Contract.Service.Id == Guid.Empty)
                {
                    throw new Exception();
                }
                else
                {

                  

                    var user = userBLL.GetById(userViewModel.Id);

                    if(user.Contract == null && user.Permissions.Where(p => p.Name != "Login").ToList().Count>0)
                    {
                        throw new BusinessException(Messages.ErrorContractUser);
                    }

                    if(user.Contract != null && user.Contract.Service.Id == userViewModel.Contract.Service.Id)
                    {
                        if(user.Contract.ExpirationDate > DateTime.Now)
                        {
                            throw new BusinessException(Messages.ErrorContractExists);
                        }
                    }
                }
                var _error = new MercadoPago.DataStructures.Generic.BadParamsError();

                


                Preference preference = new Preference();

                

                preference.Items.Add(
                  new Item()
                  {
                      Title = userViewModel.Contract.Service.Name,
                      Quantity = 1,
                      CurrencyId = CurrencyId.ARS,
                      UnitPrice = (decimal)userViewModel.Contract.Service.Price                      
                  }
                );


                preference.BinaryMode = true;

                preference.BackUrls = new BackUrls()
                {
                    Success = GlobalValues.SuccessPreference,
                    Failure = GlobalValues.FailurePreference,
                    Pending = GlobalValues.PendingPreference
                };
                preference.AutoReturn = AutoReturnType.approved;

                preference.NotificationUrl = GlobalValues.NotificationURL;

                preference.ExternalReference = userViewModel.Id.ToString() + "/" +userViewModel.Contract.Service.Id;

                var result = preference.Save();
                

                if (!result)
                {
                    _error = preference.Errors.GetValueOrDefault();
                    throw new Exception();
                }
                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, preference);

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