using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RuleBuilderInfra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addjsonValuetoRuleEtity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ruleEntities",
                newName: "JsonValue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JsonValue",
                table: "ruleEntities",
                newName: "Value");
        }
    }
}
