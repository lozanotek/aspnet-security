using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

using JWT;
using JWT.Serializers;

namespace ApiAuth.Security
{
    public class JwtTokenHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var apiToken = GetApiToken(request);
            if (apiToken == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent("Invalid API key") };
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
            if (string.IsNullOrWhiteSpace(apiToken?.Scheme) || string.IsNullOrWhiteSpace(apiToken.Value))
            {
                return null;
            }

            if (!string.Equals(apiToken.Scheme, "bearer", StringComparison.CurrentCultureIgnoreCase))
            {
                return null;
            }

            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                var token = apiToken.Value;

                var callerIdentity = decoder.DecodeToObject<CallerIdentity>(token, "secret", true);
                return callerIdentity;
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
