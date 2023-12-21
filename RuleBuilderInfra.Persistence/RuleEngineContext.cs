using Microsoft.EntityFrameworkCore;
using RuleBuilderInfra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Persistence
{
    public class RuleEngineContext : DbContext
    {
        public RuleEngineContext(DbContextOptions<RuleEngineContext> options) : base(options) { }
        public DbSet<ConditionEntity> conditionEntities { get; set; }
        public DbSet<FieldOperatorJoiningEntity> fieldOperatorJoiningEntities { get; set; }
        public DbSet<FieldTypesEntity> fieldTypesEntities { get; set; }
        public DbSet<OperatorTypesEntity> operatorTypesEntities { get; set; }


        public DbSet<RuleEntity> ruleEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConditionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FieldOperatorJoiningEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FieldTypesEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OperatorTypesEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RuleEntityConfiguration());
            modelBuilder.Ignore<ConditionRuleEntity>();
            modelBuilder.SeedingDatabase();
            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }


    public class MainDatabase : DbContext
    {
        public MainDatabase(DbContextOptions<MainDatabase> options) : base(options) { }

        public DbSet<FakeDataEntity> fakeDataEntities { get; set; }
        public DbSet<ProvincesEntity> provincesEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FakeDataEntityConfiguration());
            var fakeDataEntities = CommonUtility.LoadJsonFile<List<FakeDataEntity>>("FakeData.json");
            var provinceEntities = CommonUtility.LoadJsonFile<List<ProvincesEntity>>("Provinces.json");

            fakeDataEntities?.ForEach(fakeDataEntity =>
            {
                modelBuilder.Entity<FakeDataEntity>().HasData(fakeDataEntity);
            });
            provinceEntities?.ForEach(provinceEntity =>
            {
                modelBuilder.Entity<ProvincesEntity>().HasData(provinceEntity);
            });
            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }

}
