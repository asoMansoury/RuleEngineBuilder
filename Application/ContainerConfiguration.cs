using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RuleBuilderInfra.Application.Services.Contracts.RuleEngineer;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Application.Services.Implementations.RuleEngineer;
using RuleBuilderInfra.Application.Services.Implementations;
using RuleBuilderInfra.Application.Services;
using RuleBuilderInfra.Persistence.Repositories.Contracts.RuleEngine;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using RuleBuilderInfra.Persistence.Repositories.Implementations.RuleEngine;
using RuleBuilderInfra.Persistence.Repositories.Implementations;
using RuleBuilderInfra.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RuleBuilderInfra.Application.Mapping;
using RuleBuilderInfra.Persistence.Migrations;


namespace RuleBuilderInfra.Application
{
    public static class ContainerConfiguration
    {
        public static void DependencyInjectionEntityFramework(this IServiceCollection serviceDescriptors, string ConnectionString)
        {

            serviceDescriptors.AddDbContext<RuleEngineContext>((options) =>
            {
                options.UseSqlServer(ConnectionString);
            }
            );

            #region Repositories
            serviceDescriptors.AddTransient<IUnitOfWork, UnitOfWork>();
            serviceDescriptors.AddTransient<IConditionRepository, ConditionRepository>();
            serviceDescriptors.AddTransient<IFieldOperatorJoiningRepository, FieldOperatorJoiningRepository>();
            serviceDescriptors.AddTransient<IOperatorTypesRepository, OperatorTypesRepository>();
            serviceDescriptors.AddTransient<IFieldTypesRepository, FieldTypesRepository>();
            serviceDescriptors.AddTransient(typeof(IRuleBuilderEngineRepo<>), typeof(RuleBuilderEngineRepo<>));
            serviceDescriptors.AddTransient(typeof(ICheckEntityIsScanned<>), typeof(CheckEntityIsScanned<>));
            serviceDescriptors.AddTransient<IScanEntitiesEngineService, ScanEntitiesEngineService>();
            serviceDescriptors.AddTransient<ICallingBusinessServiceMediator, BusinessServiceDescriptors>();
            serviceDescriptors.AddTransient(typeof(IQueryBuilderRepositoryExternal<,>),typeof(QueryBuilderRepositoryExternal<,>));
            serviceDescriptors.AddSingleton<ICategoryManagerService, CategoryManagerService>();
            serviceDescriptors.AddTransient<IRuleEntityRepository, RuleEntityRepository>();
            serviceDescriptors.AddTransient<IRuleManagerService, RuleManagerService>();
            #endregion

            #region AutoMapper
            var autoMapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = autoMapperConfig.CreateMapper();
            serviceDescriptors.AddSingleton(mapper);
            #endregion

            #region Services
            serviceDescriptors.AddTransient<IConditionService, ConditionService>();
            serviceDescriptors.AddTransient<IFieldOperatorJoiningService, FieldOperatorJoiningService>();
            serviceDescriptors.AddTransient<IFieldTypesService, FieldTypesService>();
            serviceDescriptors.AddTransient<IOperatorTypesService, OperatorTypesService>();
            #endregion

            using (var scope = serviceDescriptors.BuildServiceProvider().CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<RuleEngineContext>();
                var migrationsAssembly = typeof(InitialMigration).Assembly;
                dbContext.Database.Migrate();
                dbContext.SaveChanges();
            }

        }

        public static void DependencyInjectionSqlLiteDB(this IServiceCollection serviceDescriptors, string ConnectionString)
        {

        }
    }
}
