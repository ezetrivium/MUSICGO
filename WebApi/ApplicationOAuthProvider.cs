using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;
using BLL.BLLs;
using BE.Entities;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using ViewModels.ViewModels;

namespace WebApi
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }


        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            try
            {
                UserBLL userBLL = new UserBLL();
                PermissionsBLL permissionsBLL = new PermissionsBLL();
                UserBE userToFind = new UserBE()
                {
                    UserName = context.UserName,
                    Password = context.Password
                };
                var user = await Task.Run(() => userBLL.CheckUserName(userToFind));
                user.Permissions = permissionsBLL.GetUserPermission(user);
                if (user != null)
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("Username", user.UserName));

                    string userObj = JsonConvert.SerializeObject(user);
                    identity.AddClaim(new Claim("userObject", userObj));
                    identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));
                    identity = ListPermissions(user.Permissions, identity);
                    //var additionalData = new AuthenticationProperties(new Dictionary<string, string>{
                    //    {
                    //        "role", Newtonsoft.Json.JsonConvert.SerializeObject(identity.userRoles)
                    //    }
                    //});
                    //var token = new AuthenticationTicket(identity, additionalData);
                    context.Validated(identity);
                }
                else
                    return;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }


        public ClaimsIdentity ListPermissions(IList<PermissionBE> permissions , ClaimsIdentity identity)
        {
            foreach (PermissionBE per in permissions)
            {
                if (per is PermissionsGroupBE)
                {
                    PermissionsGroupBE pergr = per as PermissionsGroupBE;
                    ListPermissions(pergr.Permissions, identity);
                }
                else
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, per.Name));
                }

            }
            return identity;
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}