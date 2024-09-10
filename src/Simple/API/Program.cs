using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddControllers();

var config = builder.Configuration;
var authority = config["ApiAuthority"];

services.AddAuthentication("API")
        .AddJwtBearer("API", options =>
        {
            options.Authority = authority;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false
            };
        });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

// Required
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
