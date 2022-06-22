using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Net.Http.Headers;

namespace Microsoft.Extensions.DependencyInjection;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // Clear all the claim type mappings
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        
        var schemeNames = new TokenAuthority[] {
                new TokenAuthority { Name = "Auth0", Issuer = configuration["Auth0:Authority"] },
                new TokenAuthority { Name = "Okta", Issuer = configuration["Okta:Authority"] },
            };

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.ForwardDefaultSelector = context =>
                    {
                        string authorization = context.Request.Headers[HeaderNames.Authorization];

                        if (!string.IsNullOrEmpty(authorization))
                        {
                            if (authorization.StartsWith(JwtBearerDefaults.AuthenticationScheme, StringComparison.OrdinalIgnoreCase))
                            {
                                var token = authorization.Substring(JwtBearerDefaults.AuthenticationScheme.Length + 1).Trim();
                                var jwtHandler = new JwtSecurityTokenHandler();

                                if (jwtHandler.CanReadToken(token))
                                {
                                    var jwtToken = jwtHandler.ReadJwtToken(token);
                                    var authority = schemeNames.FirstOrDefault(scheme => string.Equals(scheme.Issuer, jwtToken.Issuer));

                                    return authority?.Name;
                                }
                            }
                        }

                        return null;
                    };
                })
                .AddJwtBearer("Auth0", options =>
                {
                    options.Authority = configuration["Auth0:Authority"];
                    options.RefreshOnIssuerKeyNotFound = true;

                    options.TokenValidationParameters.IgnoreTrailingSlashWhenValidatingAudience = true;
                    options.TokenValidationParameters.ValidateActor = false;
                    options.TokenValidationParameters.ValidateAudience = false;
                    options.TokenValidationParameters.ValidateIssuer = false;
                    options.TokenValidationParameters.ValidateLifetime = true;
                })
                .AddJwtBearer("Okta", options =>
                {
                    options.Authority = configuration["Okta:Authority"];
                    options.RefreshOnIssuerKeyNotFound = true;

                    options.TokenValidationParameters.IgnoreTrailingSlashWhenValidatingAudience = true;
                    options.TokenValidationParameters.ValidateActor = false;
                    options.TokenValidationParameters.ValidateAudience = false;
                    options.TokenValidationParameters.ValidateIssuer = false;
                    options.TokenValidationParameters.ValidateLifetime = true;
                });

            return services;
    }
}