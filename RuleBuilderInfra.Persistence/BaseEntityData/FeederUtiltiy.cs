using RuleBuilderInfra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Persistence.BaseEntityData
{
    public class FeederUtiltiy
    {
        public virtual List<ConditionEntity> GetConditions()
        {
            var entities = new List<ConditionEntity>();
            entities.Add(new ConditionEntity()
            {
                Id = 1,
                Code = "AND",
                Name = "And"
            });
            entities.Add(new ConditionEntity()
            {
                Id = 2,
                Code = "OR",
                Name = "Or"
            });
            return entities;
        }


        public virtual List<OperatorTypesEntity> GetOperatorTypesEntities()
        {
            var entities = new List<OperatorTypesEntity>();
            entities.Add(new OperatorTypesEntity
            {
                Name = "Equal",
                OperatorTypeCode = "Eq"
            });
            entities.Add(new OperatorTypesEntity
            {
                Name = "NotEqual",
                OperatorTypeCode = "NEq"
            });
            entities.Add(new OperatorTypesEntity
            {
                Name = "GreaterThan",
                OperatorTypeCode = "Gt"
            });
            entities.Add(new OperatorTypesEntity
            {
                Name = "GreaterThanOrEqual",
                OperatorTypeCode = "Gte"
            });
            entities.Add(new OperatorTypesEntity
            {
                Name = "LessThan",
                OperatorTypeCode = "Lt"
            });
            entities.Add(new OperatorTypesEntity
            {
                Name = "LessThanOrEqual",
                OperatorTypeCode = "Lte"
            });
            entities.Add(new OperatorTypesEntity
            {
                Name = "StartsWith",
                OperatorTypeCode = "Stw"
            });
            entities.Add(new OperatorTypesEntity
            {
                Name = "Contains",
                OperatorTypeCode = "Cte"
            });
            return entities;
        }

        public virtual List<FieldOperatorJoiningEntity> GetFieldOperatorJoiningEntities()
        {
            var entities = new List<FieldOperatorJoiningEntity>();
            entities.Add(new FieldOperatorJoiningEntity
            {
                OperatorTypeCode = "Eq",
                FieldTypeCode = "Int32"
            });
            entities.Add(new FieldOperatorJoiningEntity
            {
                OperatorTypeCode = "NEq",
                FieldTypeCode = "Int32"
            });
            entities.Add(new FieldOperatorJoiningEntity
            {
                OperatorTypeCode = "Gt",
                FieldTypeCode = "Int32"
            });
            entities.Add(new FieldOperatorJoiningEntity
            {
                OperatorTypeCode = "Gte",
                FieldTypeCode = "Int32"
            });
            entities.Add(new FieldOperatorJoiningEntity
            {
                OperatorTypeCode = "Lt",
                FieldTypeCode = "Int32"
            });
            entities.Add(new FieldOperatorJoiningEntity
            {
                OperatorTypeCode = "Lte",
                FieldTypeCode = "Int32"
            });
            entities.Add(new FieldOperatorJoiningEntity
            {
                OperatorTypeCode = "Eq",
                FieldTypeCode = "ST"
            });
            entities.Add(new FieldOperatorJoiningEntity
            {
                OperatorTypeCode = "NEq",
                FieldTypeCode = "ST"
            });
            //entities.Add(new FieldOperatorJoiningEntity
            //{
            //    OperatorTypeCode = "Stw",
            //    FieldTypeCode = "ST"
            //});
            //entities.Add(new FieldOperatorJoiningEntity
            //{
            //    OperatorTypeCode = "Cte",
            //    FieldTypeCode = "ST"
            //});
            return entities;
        }

        public virtual List<FieldTypesEntity> GetFieldTypesEntities()
        {
            var entities = new List<FieldTypesEntity>();
            entities.Add(new FieldTypesEntity()
            {
                FieldType = "String",
                FieldTypeCode = "ST",
                AssemblyName = "System.String"
            });
            entities.Add(new FieldTypesEntity()
            {
                FieldType = "Int32",
                FieldTypeCode = "Int32",
                AssemblyName = "System.Int32"
            });
            entities.Add(new FieldTypesEntity()
            {
                FieldType = "Int64",
                FieldTypeCode = "Int64",
                AssemblyName = "System.Int64"
            });
            return entities;
        }

        public virtual List<FakeDataEntity> GetFakeDataEntities()
        {
            var entities = new List<FakeDataEntity>();
            int id = 1;
            entities.Add(new FakeDataEntity()
            {
                Id=id++,
                Movie = "Spider",
                Province = "Ontario",
                Distributer = "Paramond"
            });
            entities.Add(new FakeDataEntity()
            {
                Id = id++,
                Movie = "Sinderella",
                Province = "Quebec",
                Distributer = "Paramond"
            });
            entities.Add(new FakeDataEntity()
            {
                Id = id++,
                Movie = "The Notebook",
                Province = "Calgary",
                Distributer = "Disney"
            });
            entities.Add(new FakeDataEntity()
            {
                Id = id++,
                Movie = "SpiderMan",
                Province = "Ontario",
                Distributer = "Lionsgate"
            });
            entities.Add(new FakeDataEntity()
            {
                Id = id++,
                Movie = "The Notebook",
                Province = "Quebec",
                Distributer = "Lionsgate"
            });
            entities.Add(new FakeDataEntity()
            {
                Id = id++,
                Movie = "Notebook",
                Province = "Quebec",
                Distributer = "Disney"
            });
            entities.Add(new FakeDataEntity()
            {
                Id = id++,
                Movie = "Sinderella",
                Province = "Quebec",
                Distributer = "Disney"
            });

            return entities;
        }
    }
}
