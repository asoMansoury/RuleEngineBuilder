using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.PresentationModels.RuleEngineModels;
using RuleBuilderInfra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<ConditionEntity,ConditionModel>().ReverseMap();
            CreateMap<FieldTypesEntity,FieldTypesModel>().ReverseMap();
            CreateMap<OperatorTypesEntity,OperatorTypesModel>().ReverseMap();
            CreateMap<FieldOperatorJoiningEntity,FieldOperatorJoiningModel>().ReverseMap();
            CreateMap<FakeDataEntity,FakeDataModel>().ReverseMap();
            CreateMap<FieldOperatorJoiningEntity, OperatorTypesModel>().AfterMap<FieldOperatorJoiningToOperatorTypesAction>();
            CreateMap<FieldTypesModel, RuleEngineProperties>().AfterMap<FieldTypesModelToRuleEngineAction>();
        }

    }

    internal class FieldOperatorJoiningToOperatorTypesAction : IMappingAction<FieldOperatorJoiningEntity, OperatorTypesModel>
    {
        public void Process(FieldOperatorJoiningEntity source, OperatorTypesModel destination, ResolutionContext context)
        {
            destination.Name = source.OperatorTypesEntity.Name;
            destination.Code = source.OperatorTypeCode;
        }
    }

    internal class FieldTypesModelToRuleEngineAction : IMappingAction<FieldTypesModel, RuleEngineProperties>
    {
        public void Process(FieldTypesModel source, RuleEngineProperties destination, ResolutionContext context)
        {
        }
    }



}
