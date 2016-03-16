using IdentityServer.Security;
using IdentityServer3.Core.Configuration;
using Owin;
using Serilog;

namespace IdentityServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
     .WriteTo.Trace()
    .CreateLogger();

            app.Map("/identity", idsrvApp =>
            {

                idsrvApp.UseIdentityServer(new IdentityServerOptions
                {
                    RequireSsl = false,
                    SiteName = "Identity Server Demo",
                    SigningCertificate = Cert.Load(),
                    LoggingOptions = new LoggingOptions
                    {
                        EnableHttpLogging = true,
                        EnableKatanaLogging = true,
                        WebApiDiagnosticsIsVerbose = true,
                        EnableWebApiDiagnostics = true
                    },
                    Factory = new IdentityServerServiceFactory()
                                .UseInMemoryUsers(Users.Get())
                                .UseInMemoryClients(Clients.Get())
                                .UseInMemoryScopes(Scopes.Get())
                });
            });
        }
    }
}
