﻿using RuleBuilderInfra.Application.Services.Contracts.RuleEngineer;
using System.Reflection;
using RuleBuilderInfra.Application.PresentationModels;
using System.ComponentModel;
using RuleBuilderInfra.Application.PresentationModels.BuisinessEngineModels;
using RuleBuilderInfra.Persistence;
using System.Data;

namespace RuleBuilderInfra.Application.Services
{
    public class CategoryManagerService : ICategoryManagerService
    {


        private List<BusinessServiceModel> _businessServicesStore;
        private List<CategoryServiceModel> _categoryServicesStore;

        public CategoryManagerService()
        {
            RegisterInitialCategoryServices();
            RegisterInitialBusinessServices();
        }

        private void RegisterInitialCategoryServices()
        {
            if (_categoryServicesStore == null)
                _categoryServicesStore = new List<CategoryServiceModel>();

            var assemblyItem = Assembly.Load("RuleBuilderInfra.Application");
            _categoryServicesStore.Add(new CategoryServiceModel
            {
                AssemblyPath = assemblyItem!.FullName!.Split(",")[0],
                CategoryName = CategoriesAssemblyNameEnum.BaseService.GetAttribute<DescriptionAttribute>()!.Description
            });
        }

        private void RegisterInitialBusinessServices()
        {
            if (_categoryServicesStore == null)
                throw new ArgumentNullException();

            if (_businessServicesStore == null)
                _businessServicesStore = new List<BusinessServiceModel>();


            _categoryServicesStore.ForEach((assemblyItem) =>
            {
                Type GenericeTypes = typeof(BusinessEngine<>);
                Assembly assembly = Assembly.Load(assemblyItem.AssemblyPath);
                Type[] allTypes = assembly.GetTypes();
                allTypes.Where(z =>
                        z.BaseType != null &&
                        z.BaseType.Name.Equals(GenericeTypes.Name)).ToList().ForEach((derivedItem) =>
                        {
                            if (!_businessServicesStore.Any(serviceItem => serviceItem.ServiceAssembly == derivedItem!.AssemblyQualifiedName!.Split(',')[0] &&
                                                                       serviceItem.CategoryService == assemblyItem.CategoryName &&
                                                                       serviceItem.ServiceName == derivedItem.Name.Split('.')[0]))
                            {
                                _businessServicesStore.Add(new BusinessServiceModel
                                {
                                    CategoryService = assemblyItem.CategoryName,
                                    ServiceName = derivedItem.Name.Split('.')[0],
                                    ServiceAssembly = derivedItem!.AssemblyQualifiedName!.Split(',')[0]

                                });
                            }

                        });

            });

        }

        public object GetSelectedServiceType(string categoryService, string serviceName)
        {
            if (!_businessServicesStore.Any(item => item.ServiceName == serviceName && item.CategoryService == categoryService))
                throw new ArgumentNullException(nameof(GetSelectedServiceType));

            var selectedService = _businessServicesStore.SingleOrDefault(item => item.ServiceName == serviceName && item.CategoryService == categoryService)!;
            var selectedCategory = _categoryServicesStore.SingleOrDefault(category => category.CategoryName == selectedService.CategoryService)!;

            Assembly assembly = Assembly.Load(selectedCategory!.AssemblyPath!);
            Type entityType = assembly!.GetType(selectedService!.ServiceAssembly!)!;

            return Activator.CreateInstance(entityType)!;
        }

        public List<BusinessServiceModel> GetBusinessServices()
        {
            if (_businessServicesStore == null || _businessServicesStore.Count == 0)
                throw new ArgumentNullException();
            foreach (var item in _businessServicesStore)
            {
                item.InputParams = GetInputParamsForMethods(item.CategoryService, item.ServiceName);
            }
            return _businessServicesStore;
        }

        public List<CategoryServiceModel> GetAllCategories()
        {
            if (_categoryServicesStore == null || _categoryServicesStore.Count == 0)
                throw new ArgumentNullException();


            return _categoryServicesStore;
        }

        public void RegisterNewCategoryService(Assembly loadedAssembly, string categoryName)
        {
            if (_categoryServicesStore.Any(z => z.CategoryName == categoryName))
                throw new DuplicateNameException(categoryName);

            _categoryServicesStore.Add(new CategoryServiceModel
            {
                AssemblyPath = loadedAssembly!.FullName!.Split(",")[0],
                CategoryName = categoryName
            });
            RegisterInitialBusinessServices();
        }

        public void RegisterNewBusinesServices(Assembly businessAssembly, string serviceName, string categoryName)
        {
            if (_categoryServicesStore.Any(z => z.CategoryName == categoryName))
                throw new ArgumentOutOfRangeException(nameof(categoryName));

            Type GenericeTypes = typeof(BusinessEngine<>);
            Type[] allTypes = businessAssembly.GetTypes();

            allTypes.Where(z =>
                        z.BaseType != null &&
                        z.BaseType.Name.Equals(GenericeTypes.Name)).ToList().ForEach((derivedItem) =>
                        {
                            _businessServicesStore.Add(new BusinessServiceModel
                            {
                                CategoryService = categoryName,
                                ServiceName = derivedItem.Name.Split('.')[0],
                                ServiceAssembly = derivedItem!.AssemblyQualifiedName!.Split(',')[0]

                            });
                        });
        }

        public Type GetInuptBusiness(string categoryService, string serviceName)
        {
            if (!_businessServicesStore.Any(item => item.ServiceName == serviceName && item.CategoryService == categoryService))
                throw new ArgumentNullException(nameof(GetSelectedServiceType));

            var selectedService = _businessServicesStore.SingleOrDefault(item => item.ServiceName == serviceName && item.CategoryService == categoryService);
            var selectedCategory = _categoryServicesStore.SingleOrDefault(category => category.CategoryName == selectedService!.CategoryService!);

            Assembly assembly = Assembly.Load(selectedCategory!.AssemblyPath);
            Type entityType = assembly.GetType(selectedService!.ServiceAssembly!)!;
            MethodInfo performAsyncLogicMethod = entityType!.GetMethod("PerformAsyncLogic", BindingFlags.NonPublic | BindingFlags.Instance)!;

            Type returnType = performAsyncLogicMethod.ReturnType;
            Type inputBusinessType = performAsyncLogicMethod.GetParameters()[1].ParameterType;
            return inputBusinessType;
        }

        public List<BuisinessServicePropertis> GetServiceInputProperties(string CategoryService, string ServiceName, CancellationToken cancellationToken)
        {
            return GetInputParamsForMethods(CategoryService, ServiceName);
        }
        private List<BuisinessServicePropertis> GetInputParamsForMethods(string CategoryService, string ServiceName)
        {
            var types = new List<BuisinessServicePropertis>();
            var serviceBusinessType = GetInuptBusiness(CategoryService, ServiceName);
            var properties = serviceBusinessType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // Print properties and their types
            foreach (var property in properties)
            {
                Console.WriteLine($"Property: {property.Name}, Type: {property.PropertyType}");
                types.Add(new BuisinessServicePropertis
                {
                    PropertyName = property.Name,
                    PropertyType = property.PropertyType.Name
                });
            }
            return types;
        }
    }
}
