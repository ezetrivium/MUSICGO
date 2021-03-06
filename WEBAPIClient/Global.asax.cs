﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Utilities;
using WEBAPIClient;

namespace WEBAPIClient
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            MapperConfiguration.Initialize();
            MercadoPago.SDK.AccessToken = GlobalValues.AccessToken;
        }
    }
}
