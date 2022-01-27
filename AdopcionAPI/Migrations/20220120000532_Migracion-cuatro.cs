using Microsoft.EntityFrameworkCore.Migrations;

namespace AdopcionAPI.Migrations
{
    public partial class Migracioncuatro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Ciudades_DepartamentoId",
                table: "Ciudades",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Centros_CiudadId",
                table: "Centros",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_Animales_CentroId",
                table: "Animales",
                column: "CentroId");

            migrationBuilder.CreateIndex(
                name: "IX_Animales_EspecieId",
                table: "Animales",
                column: "EspecieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animales_Centros_CentroId",
                table: "Animales",
                column: "CentroId",
                principalTable: "Centros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animales_Especies_EspecieId",
                table: "Animales",
                column: "EspecieId",
                principalTable: "Especies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Centros_Ciudades_CiudadId",
                table: "Centros",
                column: "CiudadId",
                principalTable: "Ciudades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ciudades_Departamentos_DepartamentoId",
                table: "Ciudades",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animales_Centros_CentroId",
                table: "Animales");

            migrationBuilder.DropForeignKey(
                name: "FK_Animales_Especies_EspecieId",
                table: "Animales");

            migrationBuilder.DropForeignKey(
                name: "FK_Centros_Ciudades_CiudadId",
                table: "Centros");

            migrationBuilder.DropForeignKey(
                name: "FK_Ciudades_Departamentos_DepartamentoId",
                table: "Ciudades");

            migrationBuilder.DropIndex(
                name: "IX_Ciudades_DepartamentoId",
                table: "Ciudades");

            migrationBuilder.DropIndex(
                name: "IX_Centros_CiudadId",
                table: "Centros");

            migrationBuilder.DropIndex(
                name: "IX_Animales_CentroId",
                table: "Animales");

            migrationBuilder.DropIndex(
                name: "IX_Animales_EspecieId",
                table: "Animales");
        }
    }
}
