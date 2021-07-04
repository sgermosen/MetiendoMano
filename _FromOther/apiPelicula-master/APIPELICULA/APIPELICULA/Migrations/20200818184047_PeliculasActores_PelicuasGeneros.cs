using Microsoft.EntityFrameworkCore.Migrations;

namespace APIPELICULA.Migrations
{
    public partial class PeliculasActores_PelicuasGeneros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PeliclasGeneroses",
                columns: table => new
                {
                    GeneroId = table.Column<int>(nullable: false),
                    peliculaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliclasGeneroses", x => new { x.GeneroId, x.peliculaId });
                    table.ForeignKey(
                        name: "FK_PeliclasGeneroses_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliclasGeneroses_Peliculas_peliculaId",
                        column: x => x.peliculaId,
                        principalTable: "Peliculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeliculasActoreses",
                columns: table => new
                {
                    ActorId = table.Column<int>(nullable: false),
                    PelicuaId = table.Column<int>(nullable: false),
                    Personaje = table.Column<string>(nullable: true),
                    Orden = table.Column<int>(nullable: false),
                    PeliculaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculasActoreses", x => new { x.ActorId, x.PelicuaId });
                    table.ForeignKey(
                        name: "FK_PeliculasActoreses_Actores_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculasActoreses_Peliculas_PeliculaId",
                        column: x => x.PeliculaId,
                        principalTable: "Peliculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeliclasGeneroses_peliculaId",
                table: "PeliclasGeneroses",
                column: "peliculaId");

            migrationBuilder.CreateIndex(
                name: "IX_PeliculasActoreses_PeliculaId",
                table: "PeliculasActoreses",
                column: "PeliculaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeliclasGeneroses");

            migrationBuilder.DropTable(
                name: "PeliculasActoreses");
        }
    }
}
