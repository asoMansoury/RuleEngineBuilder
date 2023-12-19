using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RuleBuilderInfra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initialSteps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "fakeDataEntities",
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
                    table.PrimaryKey("PK_fakeDataEntities", x => x.Id);
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
                name: "provincesEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provincesEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ruleEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityCategoryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RuleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ruleEntities", x => x.Id);
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

            migrationBuilder.InsertData(
                table: "conditionEntities",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "AND", "And" },
                    { 2, "OR", "Or" }
                });

            migrationBuilder.InsertData(
                table: "fakeDataEntities",
                columns: new[] { "Id", "Distributer", "Movie", "Province" },
                values: new object[,]
                {
                    { 1, "Paramond", "Spider", "Ontario" },
                    { 2, "Paramond", "Sinderella", "Quebec" },
                    { 3, "Disney", "The Notebook", "Calgary" },
                    { 4, "Paramond", "Spider", "Ontario" },
                    { 5, "Lionsgate", "SpiderMan", "Ontario" },
                    { 6, "Lionsgate", "The Notebook", "Quebec" },
                    { 7, "Disney", "Notebook", "Quebec" },
                    { 8, "Disney", "Sinderella", "Quebec" }
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
                table: "provincesEntities",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "ON", "Onatrio" },
                    { 2, "QB", "Quebec" },
                    { 3, "MN", "Montreal" }
                });

            migrationBuilder.InsertData(
                table: "fieldOperatorJoiningEntities",
                columns: new[] { "FieldTypeCode", "OperatorTypeCode" },
                values: new object[,]
                {
                    { "ST", "Cte" },
                    { "Int32", "Eq" },
                    { "ST", "Eq" },
                    { "Int32", "Gt" },
                    { "Int32", "Gte" },
                    { "Int32", "Lt" },
                    { "Int32", "Lte" },
                    { "Int32", "NEq" },
                    { "ST", "NEq" },
                    { "ST", "Stw" }
                });

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
                name: "conditionEntities");

            migrationBuilder.DropTable(
                name: "fakeDataEntities");

            migrationBuilder.DropTable(
                name: "fieldOperatorJoiningEntities");

            migrationBuilder.DropTable(
                name: "provincesEntities");

            migrationBuilder.DropTable(
                name: "ruleEntities");

            migrationBuilder.DropTable(
                name: "fieldTypesEntities");

            migrationBuilder.DropTable(
                name: "operatorTypesEntities");
        }
    }
}
