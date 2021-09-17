using Microsoft.EntityFrameworkCore.Migrations;

namespace APIPELICULA.Migrations
{
    public partial class peliculas_actores2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculasActoreses_Peliculas_PeliculaId",
                table: "PeliculasActoreses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeliculasActoreses",
                table: "PeliculasActoreses");

            migrationBuilder.DropColumn(
                name: "PelicuaId",
                table: "PeliculasActoreses");

            migrationBuilder.AlterColumn<int>(
                name: "PeliculaId",
                table: "PeliculasActoreses",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeliculasActoreses",
                table: "PeliculasActoreses",
                columns: new[] { "ActorId", "PeliculaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculasActoreses_Peliculas_PeliculaId",
                table: "PeliculasActoreses",
                column: "PeliculaId",
                principalTable: "Peliculas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculasActoreses_Peliculas_PeliculaId",
                table: "PeliculasActoreses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeliculasActoreses",
                table: "PeliculasActoreses");

            migrationBuilder.AlterColumn<int>(
                name: "PeliculaId",
                table: "PeliculasActoreses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PelicuaId",
                table: "PeliculasActoreses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeliculasActoreses",
                table: "PeliculasActoreses",
                columns: new[] { "ActorId", "PelicuaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculasActoreses_Peliculas_PeliculaId",
                table: "PeliculasActoreses",
                column: "PeliculaId",
                principalTable: "Peliculas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
