using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class Tipos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TiposTecnicosTipoId",
                table: "Tecnicos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tecnicos_TiposTecnicosTipoId",
                table: "Tecnicos",
                column: "TiposTecnicosTipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tecnicos_TiposTecnicos_TiposTecnicosTipoId",
                table: "Tecnicos",
                column: "TiposTecnicosTipoId",
                principalTable: "TiposTecnicos",
                principalColumn: "TipoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tecnicos_TiposTecnicos_TiposTecnicosTipoId",
                table: "Tecnicos");

            migrationBuilder.DropIndex(
                name: "IX_Tecnicos_TiposTecnicosTipoId",
                table: "Tecnicos");

            migrationBuilder.DropColumn(
                name: "TiposTecnicosTipoId",
                table: "Tecnicos");
        }
    }
}
