
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
    public class LanguageController : BaseApiController
    {
        #region Fields
        private readonly LanguageBLL languageBLL;
        #endregion Fields

        #region Constructors
        public LanguageController()
        {
            this.languageBLL = new LanguageBLL();
        }
        #endregion Constructors

        [HttpGet]

        [Route("api/languages/get")]
        public IEnumerable<LanguageViewModel> Get()
        {
            return this.languageBLL.Get();
        }

        [HttpGet]
        [Route("api/languages/getbyid")]
        public LanguageViewModel GeByID(Guid id)
        {
            return this.languageBLL.GetById(id);
        }


        [HttpGet]

        [Route("api/languages/getbycode")]
        public LanguageViewModel GetByCode(string code)
        {
            return this.languageBLL.GetLanguageByCode(code);
        }


        [HttpGet]

        [Route("api/languages/getdictionarykeys")]
        public Dictionary<Guid,string> GetDictionaryKeys(Guid languageID)
        {
            return this.languageBLL.GetDictionaryKeys(languageID);
        }


        [HttpGet]

        [Route("api/languages/getdictionarykeysall")]
        public Dictionary<Guid, string> GetDictionaryKeysAll()
        {
            return this.languageBLL.GetDictionaryKeysAll();
        }




        [HttpPost]
        [Authorize(Roles = "AddLanguage")]
        [Route("api/language/add")]
        public IHttpActionResult Post(LanguageViewModel languageViewModel)
        {
            try
            {
                var languageOld = this.languageBLL.GetLanguageByCode(languageViewModel.Code);

                if (languageOld != null && languageOld.Id != Guid.Empty)
                {
                    throw new BusinessException(Messages.ErrorCodeLanguage);
                }

                var result = this.languageBLL.Add(languageViewModel);
                if (result == Guid.Empty)
                {
                    throw new BusinessException(Messages.ErrorAddLanguage);
                }
                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, Messages.SuccessfulUperationCrud);

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
        [Authorize(Roles = "AddLanguage")]
        [Route("api/language/adddictionary")]
        public IHttpActionResult AddDictionary(LanguageViewModel viewModel)
        {
            try
            {


                var result = this.languageBLL.AddDictionary(viewModel);
                if (!result)
                {
                    throw new BusinessException(Messages.ErrorAddDictionary);
                }
                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, Messages.SuccessfulUperationCrud);

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




        [HttpPut]
        [Authorize(Roles = "UpdateLanguage")]
        [Route("api/language/update")]
        public IHttpActionResult Put(LanguageViewModel languageViewModel)
        {
            try
            {

                var languageOld = this.languageBLL.GetLanguageByCode(languageViewModel.Code);
               

                if (languageOld != null && languageOld.Id != Guid.Empty && languageOld.Id != languageViewModel.Id)
                {
                    throw new BusinessException(Messages.ErrorCodeLanguage);
                }

                var result = this.languageBLL.Update(languageViewModel);
                if (!result)
                {
                    throw new BusinessException(Messages.ErrorUpdateLanguage);
                }
                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, Messages.SuccessfulUperationCrud);

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


        [HttpPut]
        [Authorize(Roles = "UpdateLanguage")]
        [Route("api/language/updatedictionary")]
        public IHttpActionResult UpdateDictionary(LanguageViewModel languageViewModel)
        {
            try
            {


                var result = this.languageBLL.UpdateDictionary(languageViewModel);
                if (!result)
                {
                    throw new BusinessException(Messages.ErrorUpdateDictionary);
                }
                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, Messages.SuccessfulUperationCrud);

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



        [HttpDelete]
        [Authorize(Roles = "DeleteLanguage")]
        [Route("api/language/delete/{id:Guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                var result = this.languageBLL.Delete(id);

                if (!result)
                {
                    throw new Exception(Messages.ErrorDeleteLanguage);
                }
                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, Messages.SuccessfulUperationCrud);

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