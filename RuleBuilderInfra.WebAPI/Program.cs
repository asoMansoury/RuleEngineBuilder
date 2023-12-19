using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RuleBuilderInfra.Persistence;
using RuleBuilderInfra.Application;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager? Configuration = builder.Configuration;
String connectionString = Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllers();

builder.Services.DependencyInjectionStart(connectionString);


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed((host) => true)
            .AllowCredentials());
});

var app = builder.Build();
// Configure the HTTP request pipeline.

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<RuleEngineContext>();
}
//Seeding Database with initial data 
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
