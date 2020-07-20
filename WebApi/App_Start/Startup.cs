using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;

[assembly: OwinStartup(typeof(WebApi.App_Start.Startup))]

namespace WebApi.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Enable CORS (cross origin resource sharing) for making request using browser from different domains
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,

                //The Path For generating the Toekn
                TokenEndpointPath = new PathString("/api/token"),

                //Setting the Token Expired Time (24 hours)
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),

                //MyAuthorizationServerProvider class will validate the user credentials
                Provider = new ApplicationOAuthProvider()

            };

            //Token Generations
           
            app.UseOAuthAuthorizationServer(options);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active
            });

        



            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);


        }
    }
}
