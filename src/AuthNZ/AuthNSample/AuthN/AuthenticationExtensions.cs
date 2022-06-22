using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Okta.AspNetCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthN(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = new PathString("/home");
                options.LogoutPath = new PathString("/home/signout");
                options.Cookie.Name = ".APPDEMO";
            }) 
            .AddGoogle(options =>
            {
                var googleAuthNSection =
                    configuration.GetSection("Providers:Google");

                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["Secret"];

                options.Scope.Add("profile");
                
                options.SaveTokens = true;
            })
            .AddGitHub(options =>
            {
                var googleAuthNSection =
                    configuration.GetSection("Providers:GitHub");

                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["Secret"];

                options.Scope.Add("user:email");
                
                options.SaveTokens = true;
            })
            .AddMicrosoftAccount(options =>
            {
                var msaAuthNSection =
                    configuration.GetSection("Providers:MSA");
                
                options.ClientId = msaAuthNSection["ClientId"];
                options.ClientSecret = msaAuthNSection["Secret"];
     
                options.SaveTokens = true;
            })
            .AddOktaMvc(new OktaMvcOptions
            {
                // Replace these values with your Okta configuration
                OktaDomain = configuration["Okta:OktaDomain"],
                ClientId = configuration["Okta:ClientId"],
                ClientSecret = configuration["Okta:ClientSecret"]
                //AuthorizationServerId = ""
            })
            .AddAuth0WebAppAuthentication(options => {
                options.Domain = configuration["Auth0:Domain"];
                options.ClientId = configuration["Auth0:ClientId"];
                options.ClientSecret = configuration["Auth0:ClientSecret"];
                
                options.SkipCookieMiddleware = true;
            });

        return services;
    }
}