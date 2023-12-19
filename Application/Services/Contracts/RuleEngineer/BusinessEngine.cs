using RuleBuilderInfra.Application.PresentationModels.BuisinessEngineModels;
using RuleBuilderInfra.Domain.Entities;

namespace RuleBuilderInfra.Application.Services.Contracts.RuleEngineer
{
    public abstract class BusinessEngine<TInuptBusiness>
        where TInuptBusiness : BusinessEngineModel
    {
        protected TInuptBusiness _inputValue;
        public BusinessEngine() 
        {
        
        }



        public Task<object> InvokeAsync(string outputSearchJson, BusinessEngineModel inputValue, params object[] objects)
        {
            _inputValue = (TInuptBusiness)inputValue;
            // Call the protected abstract method that derived classes must implement
            return PerformAsyncLogic(outputSearchJson, _inputValue);
        }

        protected abstract Task<object> PerformAsyncLogic(string outputSearchJson, TInuptBusiness inuptBusiness,params object[] objects);
    }


}
