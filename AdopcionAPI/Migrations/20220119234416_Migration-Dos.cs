using Microsoft.EntityFrameworkCore.Migrations;

namespace AdopcionAPI.Migrations
{
    public partial class MigrationDos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EspecieId",
                table: "Especies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DepartamentoId",
                table: "Departamentos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CiudadId",
                table: "Ciudades",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CentroId",
                table: "Centros",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Especies",
                newName: "EspecieId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Departamentos",
                newName: "DepartamentoId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Ciudades",
                newName: "CiudadId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Centros",
                newName: "CentroId");
        }
    }
}
