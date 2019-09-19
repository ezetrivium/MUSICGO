
using log4net;
using log4net.Config;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Web.Http;

namespace Institucional.WebApi
{
    public class BaseApiController : ApiController
    {
        #region Fields

        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion Fields

        #region Properties

        public ClaimsIdentity ClaimsIdentity => (ClaimsIdentity)this.User.Identity;
        public int Legajo => Convert.ToInt32(this.UserName.Substring(1));
        public string UserName => this.ClaimsIdentity.FindFirst("UserName").Value;

        #endregion Properties

        #region Constructors

        protected BaseApiController()
        {
            XmlConfigurator.Configure();
            //Log = LogManager.GetLogger(GlobalValues.WebCheckIn2LoggerName);
        }

        #endregion Constructors

        #region Protected Methods

        protected string GetNDSMessage()
        {
            return string.Format("{0} - {1}",
                Guid.NewGuid(),
                ((ClaimsIdentity)this.User.Identity).FindFirst("UserName").Value
             );
        }

        #endregion Protected Methods
    }
}