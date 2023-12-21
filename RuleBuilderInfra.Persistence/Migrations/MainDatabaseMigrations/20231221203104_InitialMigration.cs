using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RuleBuilderInfra.Persistence.Migrations.MainDatabaseMigrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "provincesEntities",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "ON", "Onatrio" },
                    { 2, "QB", "Quebec" },
                    { 3, "MN", "Montreal" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fakeDataEntities");

            migrationBuilder.DropTable(
                name: "provincesEntities");
        }
    }
}
