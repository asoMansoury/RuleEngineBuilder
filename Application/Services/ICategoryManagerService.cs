using System.Reflection;
using RuleBuilderInfra.Application.PresentationModels;

namespace RuleBuilderInfra.Application.Services
{
    public interface ICategoryManagerService
    {
        object GetSelectedServiceType(string categoryService, string serviceName);
        Type GetInuptBusiness(string categoryService, string serviceName);
        List<BusinessServiceModel> GetBusinessServices();

        List<BuisinessServicePropertis> GetServiceInputProperties(string CategoryService, string ServiceName, CancellationToken cancellationToken);
        List<CategoryServiceModel> GetAllCategories();

        void RegisterNewCategoryService(Assembly categoryService, string categoryName);

        void RegisterNewBusinesServices(Assembly businessAssembly, string serviceName, string categoryName);
    }
}
