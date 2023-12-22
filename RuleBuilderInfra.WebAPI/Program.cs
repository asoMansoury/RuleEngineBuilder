using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RuleBuilderInfra.Persistence;
using RuleBuilderInfra.Application;
using Microsoft.Extensions.DependencyInjection;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Application.Services.Implementations;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using RuleBuilderInfra.Persistence.Repositories.Implementations;
using RuleBuilderInfra.Application.Services;
using ApplicationTest.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager? Configuration = builder.Configuration;
String connectionStringMain = Configuration.GetConnectionString("DefaultConnectionMain");
String connectionString = Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<MainDatabase>((options) =>
{
    options.UseSqlServer(connectionStringMain);
});
builder.Services.AddTransient<IFakeDataRepository, FakeDataRepository>();


builder.Services.DependencyInjectionEntityFramework(connectionString);


builder.Services.AddTransient<IFakeDataService, FakeDataService>();

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
    var categoryManagerService = scope.ServiceProvider.GetService<ICategoryManagerService>();
    categoryManagerService.RegisterNewCategoryService(typeof(BusinessApplicationTest).Assembly, "ApplicationTest");

}

//Seeding Database with initial data 
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
