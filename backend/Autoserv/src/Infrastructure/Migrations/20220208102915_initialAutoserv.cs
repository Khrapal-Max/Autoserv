using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class initialAutoserv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "catalog_brands",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_catalog_brands", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "catalog_brands",
                columns: new[] { "id", "name" },
                values: new object[] { 1, "Bosh" });

            migrationBuilder.InsertData(
                table: "catalog_brands",
                columns: new[] { "id", "name" },
                values: new object[] { 2, "HEPU" });

            migrationBuilder.InsertData(
                table: "catalog_brands",
                columns: new[] { "id", "name" },
                values: new object[] { 3, "MANN" });

            migrationBuilder.CreateIndex(
                name: "ix_catalog_brands_name",
                table: "catalog_brands",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "catalog_brands");
        }
    }
}
