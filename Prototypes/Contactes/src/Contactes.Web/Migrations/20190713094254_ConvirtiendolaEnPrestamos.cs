using Microsoft.EntityFrameworkCore.Migrations;

namespace Contactes.Web.Migrations
{
    public partial class ConvirtiendolaEnPrestamos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MontoAdeudado",
                table: "Personas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MontoAdeudado",
                table: "Personas");
        }
    }
}
