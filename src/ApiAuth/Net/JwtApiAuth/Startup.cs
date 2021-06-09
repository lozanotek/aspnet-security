using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.IdentityModel.Tokens;

using System.Threading.Tasks;

namespace JwtApiAuth
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
            services.AddControllersWithViews();

            services
               .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(x =>
               {
                   // Disabled only for local dev
                   x.RequireHttpsMetadata = false;
                   x.Authority = Configuration["API:Authority"];

                   x.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidAudience = Configuration["API:Audience"]
                   };

                   x.Events = new JwtBearerEvents();
                   x.Events.OnAuthenticationFailed += AuthenticationFailed;
                   x.Events.OnChallenge += Challenge;
                   x.Events.OnTokenValidated += TokenValidated;
               });
        }

        private Task TokenValidated(TokenValidatedContext arg)
        {
            return Task.FromResult(0);
        }

        private Task Challenge(JwtBearerChallengeContext arg)
        {
            return Task.FromResult(0);
        }

        private Task AuthenticationFailed(AuthenticationFailedContext arg)
        {
            return Task.FromResult(0);
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
