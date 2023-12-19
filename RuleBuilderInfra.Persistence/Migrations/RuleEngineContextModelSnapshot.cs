﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RuleBuilderInfra.Persistence;

#nullable disable

namespace RuleBuilderInfra.Persistence.Migrations
{
    [DbContext(typeof(RuleEngineContext))]
    partial class RuleEngineContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RuleBuilderInfra.Domain.Entities.ConditionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("conditionEntities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "AND",
                            Name = "And"
                        },
                        new
                        {
                            Id = 2,
                            Code = "OR",
                            Name = "Or"
                        });
                });

            modelBuilder.Entity("RuleBuilderInfra.Domain.Entities.FakeDataEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Distributer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Movie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("fakeDataEntities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Distributer = "Paramond",
                            Movie = "Spider",
                            Province = "Ontario"
                        },
                        new
                        {
                            Id = 2,
                            Distributer = "Paramond",
                            Movie = "Sinderella",
                            Province = "Quebec"
                        },
                        new
                        {
                            Id = 3,
                            Distributer = "Disney",
                            Movie = "The Notebook",
                            Province = "Calgary"
                        },
                        new
                        {
                            Id = 4,
                            Distributer = "Paramond",
                            Movie = "Spider",
                            Province = "Ontario"
                        },
                        new
                        {
                            Id = 5,
                            Distributer = "Lionsgate",
                            Movie = "SpiderMan",
                            Province = "Ontario"
                        },
                        new
                        {
                            Id = 6,
                            Distributer = "Lionsgate",
                            Movie = "The Notebook",
                            Province = "Quebec"
                        },
                        new
                        {
                            Id = 7,
                            Distributer = "Disney",
                            Movie = "Notebook",
                            Province = "Quebec"
                        },
                        new
                        {
                            Id = 8,
                            Distributer = "Disney",
                            Movie = "Sinderella",
                            Province = "Quebec"
                        });
                });

            modelBuilder.Entity("RuleBuilderInfra.Domain.Entities.FieldOperatorJoiningEntity", b =>
                {
                    b.Property<string>("OperatorTypeCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FieldTypeCode")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OperatorTypeCode", "FieldTypeCode");

                    b.HasIndex("FieldTypeCode");

                    b.ToTable("fieldOperatorJoiningEntities");

                    b.HasData(
                        new
                        {
                            OperatorTypeCode = "Eq",
                            FieldTypeCode = "Int32"
                        },
                        new
                        {
                            OperatorTypeCode = "NEq",
                            FieldTypeCode = "Int32"
                        },
                        new
                        {
                            OperatorTypeCode = "Gt",
                            FieldTypeCode = "Int32"
                        },
                        new
                        {
                            OperatorTypeCode = "Gte",
                            FieldTypeCode = "Int32"
                        },
                        new
                        {
                            OperatorTypeCode = "Lt",
                            FieldTypeCode = "Int32"
                        },
                        new
                        {
                            OperatorTypeCode = "Lte",
                            FieldTypeCode = "Int32"
                        },
                        new
                        {
                            OperatorTypeCode = "Eq",
                            FieldTypeCode = "ST"
                        },
                        new
                        {
                            OperatorTypeCode = "NEq",
                            FieldTypeCode = "ST"
                        },
                        new
                        {
                            OperatorTypeCode = "Stw",
                            FieldTypeCode = "ST"
                        },
                        new
                        {
                            OperatorTypeCode = "Cte",
                            FieldTypeCode = "ST"
                        });
                });

            modelBuilder.Entity("RuleBuilderInfra.Domain.Entities.FieldTypesEntity", b =>
                {
                    b.Property<string>("FieldTypeCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssemblyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FieldType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FieldTypeCode");

                    b.HasIndex("FieldTypeCode")
                        .IsUnique();

                    b.ToTable("fieldTypesEntities");

                    b.HasData(
                        new
                        {
                            FieldTypeCode = "ST",
                            AssemblyName = "System.String",
                            FieldType = "String"
                        },
                        new
                        {
                            FieldTypeCode = "Int32",
                            AssemblyName = "System.Int32",
                            FieldType = "Int32"
                        },
                        new
                        {
                            FieldTypeCode = "Int64",
                            AssemblyName = "System.Int64",
                            FieldType = "Int64"
                        });
                });

            modelBuilder.Entity("RuleBuilderInfra.Domain.Entities.OperatorTypesEntity", b =>
                {
                    b.Property<string>("OperatorTypeCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OperatorTypeCode");

                    b.ToTable("operatorTypesEntities");

                    b.HasData(
                        new
                        {
                            OperatorTypeCode = "Eq",
                            Name = "Equal"
                        },
                        new
                        {
                            OperatorTypeCode = "NEq",
                            Name = "NotEqual"
                        },
                        new
                        {
                            OperatorTypeCode = "Gt",
                            Name = "GreaterThan"
                        },
                        new
                        {
                            OperatorTypeCode = "Gte",
                            Name = "GreaterThanOrEqual"
                        },
                        new
                        {
                            OperatorTypeCode = "Lt",
                            Name = "LessThan"
                        },
                        new
                        {
                            OperatorTypeCode = "Lte",
                            Name = "LessThanOrEqual"
                        },
                        new
                        {
                            OperatorTypeCode = "Stw",
                            Name = "StartsWith"
                        },
                        new
                        {
                            OperatorTypeCode = "Cte",
                            Name = "Contains"
                        });
                });

            modelBuilder.Entity("RuleBuilderInfra.Domain.Entities.ProvincesEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("provincesEntities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "ON",
                            Name = "Onatrio"
                        },
                        new
                        {
                            Id = 2,
                            Code = "QB",
                            Name = "Quebec"
                        },
                        new
                        {
                            Id = 3,
                            Code = "MN",
                            Name = "Montreal"
                        });
                });

            modelBuilder.Entity("RuleBuilderInfra.Domain.Entities.RuleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryService")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConditionJson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EntityCategoryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EntityCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JsonValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("JsonValue");

                    b.Property<string>("RuleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ruleEntities");
                });

            modelBuilder.Entity("RuleBuilderInfra.Domain.Entities.FieldOperatorJoiningEntity", b =>
                {
                    b.HasOne("RuleBuilderInfra.Domain.Entities.FieldTypesEntity", "FieldTypesEntity")
                        .WithMany("FieldOperatorJoiningEntities")
                        .HasForeignKey("FieldTypeCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RuleBuilderInfra.Domain.Entities.OperatorTypesEntity", "OperatorTypesEntity")
                        .WithMany("FieldOperatorJoiningEntities")
                        .HasForeignKey("OperatorTypeCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FieldTypesEntity");

                    b.Navigation("OperatorTypesEntity");
                });

            modelBuilder.Entity("RuleBuilderInfra.Domain.Entities.FieldTypesEntity", b =>
                {
                    b.Navigation("FieldOperatorJoiningEntities");
                });

            modelBuilder.Entity("RuleBuilderInfra.Domain.Entities.OperatorTypesEntity", b =>
                {
                    b.Navigation("FieldOperatorJoiningEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
