using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RuleBuilderInfra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initialRuleContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "actionEntities",
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
                    table.PrimaryKey("PK_actionEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "conditionEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conditionEntities", x => x.Id);
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
                name: "fieldTypesEntities",
                columns: table => new
                {
                    FieldTypeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FieldType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssemblyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fieldTypesEntities", x => x.FieldTypeCode);
                });

            migrationBuilder.CreateTable(
                name: "operatorTypesEntities",
                columns: table => new
                {
                    OperatorTypeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operatorTypesEntities", x => x.OperatorTypeCode);
                });

            migrationBuilder.CreateTable(
                name: "ruleEntities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityCategoryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RuleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JsonValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ruleEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "actionPropertisEntities",
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
                    table.PrimaryKey("PK_actionPropertisEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_actionPropertisEntities_actionEntities_ActionEntityID",
                        column: x => x.ActionEntityID,
                        principalTable: "actionEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fieldOperatorJoiningEntities",
                columns: table => new
                {
                    OperatorTypeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FieldTypeCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fieldOperatorJoiningEntities", x => new { x.OperatorTypeCode, x.FieldTypeCode });
                    table.ForeignKey(
                        name: "FK_fieldOperatorJoiningEntities_fieldTypesEntities_FieldTypeCode",
                        column: x => x.FieldTypeCode,
                        principalTable: "fieldTypesEntities",
                        principalColumn: "FieldTypeCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fieldOperatorJoiningEntities_operatorTypesEntities_OperatorTypeCode",
                        column: x => x.OperatorTypeCode,
                        principalTable: "operatorTypesEntities",
                        principalColumn: "OperatorTypeCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionRuleEntity",
                columns: table => new
                {
                    RuleEntityID = table.Column<long>(type: "bigint", nullable: false),
                    ActionEntityID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionRuleEntity", x => new { x.RuleEntityID, x.ActionEntityID });
                    table.ForeignKey(
                        name: "FK_ActionRuleEntity_actionEntities_ActionEntityID",
                        column: x => x.ActionEntityID,
                        principalTable: "actionEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionRuleEntity_ruleEntities_RuleEntityID",
                        column: x => x.RuleEntityID,
                        principalTable: "ruleEntities",
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
                    ConditionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    RuleEntityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionRuleEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConditionRuleEntities_ConditionRuleEntities_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ConditionRuleEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConditionRuleEntities_ruleEntities_RuleEntityId",
                        column: x => x.RuleEntityId,
                        principalTable: "ruleEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ActionRulePropertiesEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RuleEntityId = table.Column<long>(type: "bigint", nullable: false),
                    ActionPropertyEntityId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionRulePropertiesEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionRulePropertiesEntity_actionPropertisEntities_ActionPropertyEntityId",
                        column: x => x.ActionPropertyEntityId,
                        principalTable: "actionPropertisEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionRulePropertiesEntity_ruleEntities_RuleEntityId",
                        column: x => x.RuleEntityId,
                        principalTable: "ruleEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                table: "conditionEntities",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "AND", "And" },
                    { 2, "OR", "Or" }
                });

            migrationBuilder.InsertData(
                table: "fieldTypesEntities",
                columns: new[] { "FieldTypeCode", "AssemblyName", "FieldType" },
                values: new object[,]
                {
                    { "Int32", "System.Int32", "Int32" },
                    { "Int64", "System.Int64", "Int64" },
                    { "ST", "System.String", "String" }
                });

            migrationBuilder.InsertData(
                table: "operatorTypesEntities",
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
                table: "fieldOperatorJoiningEntities",
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
                name: "IX_actionPropertisEntities_ActionEntityID",
                table: "actionPropertisEntities",
                column: "ActionEntityID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionRuleEntity_ActionEntityID",
                table: "ActionRuleEntity",
                column: "ActionEntityID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionRulePropertiesEntity_ActionPropertyEntityId",
                table: "ActionRulePropertiesEntity",
                column: "ActionPropertyEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionRulePropertiesEntity_RuleEntityId",
                table: "ActionRulePropertiesEntity",
                column: "RuleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionRuleEntities_ParentId",
                table: "ConditionRuleEntities",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionRuleEntities_RuleEntityId",
                table: "ConditionRuleEntities",
                column: "RuleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_fieldOperatorJoiningEntities_FieldTypeCode",
                table: "fieldOperatorJoiningEntities",
                column: "FieldTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_fieldTypesEntities_FieldTypeCode",
                table: "fieldTypesEntities",
                column: "FieldTypeCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionRuleEntity");

            migrationBuilder.DropTable(
                name: "ActionRulePropertiesEntity");

            migrationBuilder.DropTable(
                name: "conditionEntities");

            migrationBuilder.DropTable(
                name: "ConditionRuleEntities");

            migrationBuilder.DropTable(
                name: "FakeDataEntity");

            migrationBuilder.DropTable(
                name: "fieldOperatorJoiningEntities");

            migrationBuilder.DropTable(
                name: "actionPropertisEntities");

            migrationBuilder.DropTable(
                name: "ruleEntities");

            migrationBuilder.DropTable(
                name: "fieldTypesEntities");

            migrationBuilder.DropTable(
                name: "operatorTypesEntities");

            migrationBuilder.DropTable(
                name: "actionEntities");
        }
    }
}
