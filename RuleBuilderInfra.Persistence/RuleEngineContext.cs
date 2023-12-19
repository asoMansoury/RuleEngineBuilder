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
        public DbSet<FakeDataEntity> fakeDataEntities { get; set; }
        public DbSet<FieldOperatorJoiningEntity> fieldOperatorJoiningEntities { get; set; }
        public DbSet<FieldTypesEntity> fieldTypesEntities { get; set; }
        public DbSet<OperatorTypesEntity> operatorTypesEntities { get; set; }

        public DbSet<ProvincesEntity> provincesEntities { get; set; }
        public DbSet<RuleEntity> ruleEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ConditionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FakeDataEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FieldOperatorJoiningEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FieldTypesEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OperatorTypesEntityConfiguration());
            modelBuilder.ApplyConfiguration(new  ProvincesEntityConfiguration()); 
            modelBuilder.ApplyConfiguration(new RuleEntityConfiguration());
            modelBuilder.Ignore<ConditionRuleEntity>(); 
            modelBuilder.SeedingDatabase();
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
