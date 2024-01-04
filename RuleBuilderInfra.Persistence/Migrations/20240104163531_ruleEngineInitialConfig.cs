using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RuleBuilderInfra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ruleEngineInitialConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionEntities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceAssembly = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConditionEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FakeDataEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Movie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distributer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FakeDataEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FieldTypesEntities",
                columns: table => new
                {
                    FieldTypeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FieldType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssemblyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldTypesEntities", x => x.FieldTypeCode);
                });

            migrationBuilder.CreateTable(
                name: "OperatorTypesEntities",
                columns: table => new
                {
                    OperatorTypeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatorTypesEntities", x => x.OperatorTypeCode);
                });

            migrationBuilder.CreateTable(
                name: "RuleEntities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityCategoryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RuleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CategoryService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActionPropertisEntities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ActionEntityID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionPropertisEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionPropertisEntities_ActionEntities_ActionEntityID",
                        column: x => x.ActionEntityID,
                        principalTable: "ActionEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldOperatorJoiningEntities",
                columns: table => new
                {
                    OperatorTypeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FieldTypeCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldOperatorJoiningEntities", x => new { x.OperatorTypeCode, x.FieldTypeCode });
                    table.ForeignKey(
                        name: "FK_FieldOperatorJoiningEntities_FieldTypesEntities_FieldTypeCode",
                        column: x => x.FieldTypeCode,
                        principalTable: "FieldTypesEntities",
                        principalColumn: "FieldTypeCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FieldOperatorJoiningEntities_OperatorTypesEntities_OperatorTypeCode",
                        column: x => x.OperatorTypeCode,
                        principalTable: "OperatorTypesEntities",
                        principalColumn: "OperatorTypeCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionRuleEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RuleEntityID = table.Column<long>(type: "bigint", nullable: false),
                    ActionEntityID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionRuleEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionRuleEntity_ActionEntities_ActionEntityID",
                        column: x => x.ActionEntityID,
                        principalTable: "ActionEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionRuleEntity_RuleEntities_RuleEntityID",
                        column: x => x.RuleEntityID,
                        principalTable: "RuleEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConditionRuleEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Operator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ConditionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    RuleEntityId = table.Column<long>(type: "bigint", nullable: true),
                    ConditionEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionRuleEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConditionRuleEntities_ConditionEntities_ConditionEntityId",
                        column: x => x.ConditionEntityId,
                        principalTable: "ConditionEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConditionRuleEntities_ConditionRuleEntities_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ConditionRuleEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConditionRuleEntities_RuleEntities_RuleEntityId",
                        column: x => x.RuleEntityId,
                        principalTable: "RuleEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ActionRulePropertiesEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionRuleEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionPropertyEntityId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionRulePropertiesEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionRulePropertiesEntity_ActionPropertisEntities_ActionPropertyEntityId",
                        column: x => x.ActionPropertyEntityId,
                        principalTable: "ActionPropertisEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionRulePropertiesEntity_ActionRuleEntity_ActionRuleEntityId",
                        column: x => x.ActionRuleEntityId,
                        principalTable: "ActionRuleEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "ConditionEntities",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "AND", "And" },
                    { 2, "OR", "Or" }
                });

            migrationBuilder.InsertData(
                table: "FakeDataEntity",
                columns: new[] { "Id", "Distributer", "Movie", "Province" },
                values: new object[,]
                {
                    { 1, "Paramond", "Spider", "Ontario" },
                    { 2, "Paramond", "Sinderella", "Quebec" },
                    { 3, "Disney", "The Notebook", "Calgary" },
                    { 4, "Lionsgate", "SpiderMan", "Ontario" },
                    { 5, "Lionsgate", "The Notebook", "Quebec" },
                    { 6, "Disney", "Notebook", "Quebec" },
                    { 7, "Disney", "Sinderella", "Quebec" }
                });

            migrationBuilder.InsertData(
                table: "FieldTypesEntities",
                columns: new[] { "FieldTypeCode", "AssemblyName", "FieldType" },
                values: new object[,]
                {
                    { "Int32", "System.Int32", "Int32" },
                    { "Int64", "System.Int64", "Int64" },
                    { "ST", "System.String", "String" }
                });

            migrationBuilder.InsertData(
                table: "OperatorTypesEntities",
                columns: new[] { "OperatorTypeCode", "Name" },
                values: new object[,]
                {
                    { "Cte", "Contains" },
                    { "Eq", "Equal" },
                    { "Gt", "GreaterThan" },
                    { "Gte", "GreaterThanOrEqual" },
                    { "Lt", "LessThan" },
                    { "Lte", "LessThanOrEqual" },
                    { "NEq", "NotEqual" },
                    { "Stw", "StartsWith" }
                });

            migrationBuilder.InsertData(
                table: "FieldOperatorJoiningEntities",
                columns: new[] { "FieldTypeCode", "OperatorTypeCode" },
                values: new object[,]
                {
                    { "Int32", "Eq" },
                    { "ST", "Eq" },
                    { "Int32", "Gt" },
                    { "Int32", "Gte" },
                    { "Int32", "Lt" },
                    { "Int32", "Lte" },
                    { "Int32", "NEq" },
                    { "ST", "NEq" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionPropertisEntities_ActionEntityID",
                table: "ActionPropertisEntities",
                column: "ActionEntityID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionRuleEntity_ActionEntityID",
                table: "ActionRuleEntity",
                column: "ActionEntityID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionRuleEntity_RuleEntityID",
                table: "ActionRuleEntity",
                column: "RuleEntityID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionRulePropertiesEntity_ActionPropertyEntityId",
                table: "ActionRulePropertiesEntity",
                column: "ActionPropertyEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionRulePropertiesEntity_ActionRuleEntityId",
                table: "ActionRulePropertiesEntity",
                column: "ActionRuleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionRuleEntities_ConditionEntityId",
                table: "ConditionRuleEntities",
                column: "ConditionEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionRuleEntities_ParentId",
                table: "ConditionRuleEntities",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionRuleEntities_RuleEntityId",
                table: "ConditionRuleEntities",
                column: "RuleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldOperatorJoiningEntities_FieldTypeCode",
                table: "FieldOperatorJoiningEntities",
                column: "FieldTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_FieldTypesEntities_FieldTypeCode",
                table: "FieldTypesEntities",
                column: "FieldTypeCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionRulePropertiesEntity");

            migrationBuilder.DropTable(
                name: "ConditionRuleEntities");

            migrationBuilder.DropTable(
                name: "FakeDataEntity");

            migrationBuilder.DropTable(
                name: "FieldOperatorJoiningEntities");

            migrationBuilder.DropTable(
                name: "ActionPropertisEntities");

            migrationBuilder.DropTable(
                name: "ActionRuleEntity");

            migrationBuilder.DropTable(
                name: "ConditionEntities");

            migrationBuilder.DropTable(
                name: "FieldTypesEntities");

            migrationBuilder.DropTable(
                name: "OperatorTypesEntities");

            migrationBuilder.DropTable(
                name: "ActionEntities");

            migrationBuilder.DropTable(
                name: "RuleEntities");
        }
    }
}
