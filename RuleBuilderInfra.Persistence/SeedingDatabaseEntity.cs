using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using RuleBuilderInfra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Persistence
{
    public static class SeedingDatabaseEntity
    {
        public static void SeedingDatabase(this ModelBuilder modelBuilder)
        {
            var conditionData = CommonUtility.LoadJsonFile<List<ConditionEntity>>("Conditions.json");
            var operatorTypesData = CommonUtility.LoadJsonFile<List<OperatorTypesEntity>>("OperatorTypes.json");
            var FieldOperatorJoiningData = CommonUtility.LoadJsonFile<List<FieldOperatorJoiningEntity>>("FieldOperatorJoining.json");
            var fieldTypesData = CommonUtility.LoadJsonFile<List<FieldTypesEntity>>("FieldTypes.json");
            var fakeDataEntities = CommonUtility.LoadJsonFile<List<FakeDataEntity>>("FakeData.json");



            conditionData?.ForEach(conditionData =>
            {
                modelBuilder.Entity<ConditionEntity>().HasData(conditionData);
            });

            operatorTypesData?.ForEach(operatorEntity =>
            {
                modelBuilder.Entity<OperatorTypesEntity>().HasData(operatorEntity);
            });

            fieldTypesData?.ForEach(fieldEntity =>
            {
                modelBuilder.Entity<FieldTypesEntity>().HasData(fieldEntity);
            });

            FieldOperatorJoiningData?.ForEach(fieldOperatorEntity =>
            {
                modelBuilder.Entity<FieldOperatorJoiningEntity>().HasData(fieldOperatorEntity); 
            });

            fakeDataEntities?.ForEach(fakeDataEntity =>{
                modelBuilder.Entity<FakeDataEntity>().HasData(fakeDataEntity);
            });


        }
    }
}
