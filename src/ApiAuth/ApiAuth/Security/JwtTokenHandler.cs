using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ApiAuth.Security
{
    public class JwtTokenHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var apiToken = GetApiToken(request);
            if (apiToken == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized) {Content = new StringContent("Invalid API key")};
            }

            var callerIdentity = ParseToken(apiToken);
            if (callerIdentity == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent("Invalid API key") };
            }

            var principal = new SecurityPrincipal(callerIdentity);
            Thread.CurrentPrincipal = principal;
            HttpContext.Current.User = principal;

            return await base.SendAsync(request, cancellationToken);
        }

        private CallerIdentity ParseToken(ApiToken apiToken)
        {
            if (apiToken == null || 
                string.IsNullOrWhiteSpace(apiToken.Scheme) || 
                string.IsNullOrWhiteSpace(apiToken.Value))
            {
                return null;
            }

            if (!string.Equals(apiToken.Scheme, "bearer", StringComparison.CurrentCultureIgnoreCase))
            {
                return null;
            }

            try
            {
                var callerInfo = JWT.JsonWebToken.DecodeToObject<CallerIdentity>(apiToken.Value, "secret");
                return callerInfo;
            }
            catch
            {
                // Lame, I know...but it's a demo
                return null;   
            }
        }

        protected virtual ApiToken GetApiToken(HttpRequestMessage request)
        {
            var authHeader = request.Headers.Authorization;
            if (authHeader == null)
            {
                return null;
            }

            return new ApiToken
            {
                Scheme = authHeader.Scheme,
                Value = authHeader.Parameter
            };
        }
    }
}
