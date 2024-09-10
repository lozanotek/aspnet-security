using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.IdentityModel.Tokens;

using ApiAuth.Models;

namespace ApiAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddControllersWithViews();

            var builder = services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

            builder
                .AddJwtBearer(x =>
                {
                    // Disabled only for local dev
                    x.RequireHttpsMetadata = false;

                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = false,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateActor = false,
                        IssuerSigningKeyResolver = ResolveSecurityKey
                    };
                });
        }

        private IEnumerable<SecurityKey> ResolveSecurityKey(string token, SecurityToken securityToken, string kid, TokenValidationParameters validationParameter)
        {
            if (string.IsNullOrWhiteSpace(kid))
            {
                return null;
            }

            var apiConfig = new ApiConfiguration();
            Configuration.GetSection("ApiConfiguration").Bind(apiConfig);

            var clients = apiConfig.Clients;
            if (clients == null)
            {
                return null;
            }

            var client = clients.SingleOrDefault(c => c.Key == kid);
            if (client == null)
            {
                return null;
            }

            var data = Encoding.ASCII.GetBytes(client.Secret);
            var key = new SymmetricSecurityKey(data);

            return new[] { key };
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                });
            });
        }
    }
}
