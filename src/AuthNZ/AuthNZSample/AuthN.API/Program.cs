var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
services.AddApiAuthentication(configuration)
        .AddPermissions();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Force authN/authZ in all controllers
app.MapControllers().RequireAuthorization();

app.Run();