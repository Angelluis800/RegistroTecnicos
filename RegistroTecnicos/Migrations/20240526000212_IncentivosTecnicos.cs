using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class IncentivosTecnicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tecnicos_IncentivoId",
                table: "Tecnicos");

            migrationBuilder.AddColumn<int>(
                name: "TecnicoId",
                table: "Incentivos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tecnicos_IncentivoId",
                table: "Tecnicos",
                column: "IncentivoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tecnicos_IncentivoId",
                table: "Tecnicos");

            migrationBuilder.DropColumn(
                name: "TecnicoId",
                table: "Incentivos");

            migrationBuilder.CreateIndex(
                name: "IX_Tecnicos_IncentivoId",
                table: "Tecnicos",
                column: "IncentivoId");
        }
    }
}
