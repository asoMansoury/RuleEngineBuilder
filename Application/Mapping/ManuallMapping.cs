﻿using Azure.Core;
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
    internal class ManuallMapping
    {
        public static RuleEngineProperties Map(FieldTypesEntity fieldTypesEntities)
        {
            var result = new RuleEngineProperties();
            result.FieldType = fieldTypesEntities.FieldType;
            result.FieldTypeCode = fieldTypesEntities.FieldTypeCode;
            return result;
        }
        public static List<FieldOperatorJoiningModel> Map(List<FieldOperatorJoiningEntity> items)
        {
            var result = new List<FieldOperatorJoiningModel>();
            items.ForEach((item) =>
            {
                result.Add(new FieldOperatorJoiningModel
                {
                    Code = item.OperatorTypesEntity.OperatorTypeCode,
                    Name = item.OperatorTypesEntity.Name,
                });
            });
            return result;
        }

        public static List<BusinessServiceModel> Map(List<ActionEntity> items)
        {
            var result = new List<BusinessServiceModel>();
            items.ForEach((item) => {
                result.Add(new BusinessServiceModel
                {
                    CategoryService = item.CategoryService,
                    ServiceAssembly = item.ServiceAssembly,
                    ServiceName = item.ServiceName,
                    InputParams = item.ActionPropertis.ConvertAll(x=> new BuisinessServicePropertis
                    {
                        PropertyName = x.PropertyName,
                        PropertyType = x.PropertyType,
                    })
                });
            });
            return result;
        }

    }
}
