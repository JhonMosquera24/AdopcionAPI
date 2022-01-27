using Microsoft.EntityFrameworkCore.Migrations;

namespace AdopcionAPI.Migrations
{
    public partial class CorrecionTipodato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adopciones_Animales_MascotaId",
                table: "Adopciones");

            migrationBuilder.DropIndex(
                name: "IX_Adopciones_MascotaId",
                table: "Adopciones");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Adopciones_MascotaId",
                table: "Adopciones",
                column: "MascotaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adopciones_Animales_MascotaId",
                table: "Adopciones",
                column: "MascotaId",
                principalTable: "Animales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
