using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Application.Services.Implementations;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using RuleBuilderInfra.Persistence.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RuleBuilderInfra.Application.Mapping;
using RuleBuilderInfra.Application.Services.Contracts.RuleEngineer;
using RuleBuilderInfra.Application.Services.Implementations.RuleEngineer;
using RuleBuilderInfra.Persistence.Repositories.Contracts.RuleEngine;
using RuleBuilderInfra.Persistence.Repositories.Implementations.RuleEngine;
using RuleBuilderInfra.Application.Services;

namespace RuleBuilderInfra.Infrastructure
{
    public static class ContainerConfiguration2
    {
        public static void DependencyInjectionStart(this IServiceCollection serviceDescriptors, string ConnectionString)
        {
            serviceDescriptors.AddDbContext<RuleEngineContext>((options) =>
             {
                 options.UseSqlServer(ConnectionString);
             }
            );

            #region Repositories
            serviceDescriptors.AddTransient<IUnitOfWork, UnitOfWork>();
            serviceDescriptors.AddTransient<IConditionRepository, ConditionRepository>();
            serviceDescriptors.AddTransient<IFakeDataRepository, FakeDataRepository>();
            serviceDescriptors.AddTransient<IFieldOperatorJoiningRepository, FieldOperatorJoiningRepository>();
            serviceDescriptors.AddTransient<IOperatorTypesRepository, OperatorTypesRepository>();
            serviceDescriptors.AddTransient<IFieldTypesRepository, FieldTypesRepository>();
            serviceDescriptors.AddTransient(typeof(IRuleBuilderEngineService<,>),typeof(RuleBuilderEngineService<,>));
            serviceDescriptors.AddTransient(typeof(IQueryBuilderRepository<>), typeof(QueryBuilderRepository<>));
            serviceDescriptors.AddTransient(typeof(IRuleBuilderEngineRepo<>), typeof(RuleBuilderEngineRepo<>));
            serviceDescriptors.AddTransient(typeof(ICheckEntityIsScanned<>),typeof( CheckEntityIsScanned<>));
            serviceDescriptors.AddTransient< IScanEntitiesEngineService,ScanEntitiesEngineService>();
            serviceDescriptors.AddTransient<ICallingBusinessServiceMediator, BusinessServiceDescriptors>();
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
            serviceDescriptors.AddTransient<IFakeDataService, FakeDataService>();
            serviceDescriptors.AddTransient<IFieldOperatorJoiningService, FieldOperatorJoiningService>();
            serviceDescriptors.AddTransient<IFieldTypesService, FieldTypesService>();
            serviceDescriptors.AddTransient<IOperatorTypesService, OperatorTypesService>();
            #endregion

        }
    }
}
