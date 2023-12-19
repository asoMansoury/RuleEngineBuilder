using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RuleBuilderInfra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addconditionJsonFieldtoRuleEtity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConditionJson",
                table: "ruleEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConditionJson",
                table: "ruleEntities");
        }
    }
}
