using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Thinktecture.IdentityModel.Client;

namespace IdentityClient
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = "sub";
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = SecurityConstants.AuthenticationType
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "client1",
                Authority = SecurityConstants.AuthorityUri,
                RedirectUri = "http://localhost:4567/",
                PostLogoutRedirectUri = "http://localhost:4567",
                Scope = SecurityConstants.Scope,
                ResponseType = SecurityConstants.HydridResponseType,
                SignInAsAuthenticationType = SecurityConstants.AuthenticationType,
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    AuthorizationCodeReceived = AuthCodeReceived
                }
            });
        }

        protected async Task AuthCodeReceived(AuthorizationCodeReceivedNotification notification)
        {
            var identity = notification.AuthenticationTicket.Identity;

            var claimsIdentity = new ClaimsIdentity(identity.AuthenticationType, "email", "role");

            var userInfoClient = new UserInfoClient(new Uri(SecurityConstants.UserInfoUri), 
                notification.ProtocolMessage.AccessToken);

            var userInfo = await userInfoClient.GetAsync();
            foreach (var claim in userInfo.Claims)
            {
                claimsIdentity.AddClaim(new Claim(claim.Item1, claim.Item2));
            }

            // Save the id_token for the application
            claimsIdentity.AddClaim(new Claim("id_token", notification.ProtocolMessage.IdToken));

            notification.AuthenticationTicket = new AuthenticationTicket(
                claimsIdentity,
                notification.AuthenticationTicket.Properties);
        }
    }
}
