using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class IncentivosOpcional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tecnicos_Incentivos_IncentivoId",
                table: "Tecnicos");

            migrationBuilder.AlterColumn<int>(
                name: "IncentivoId",
                table: "Tecnicos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Tecnicos_Incentivos_IncentivoId",
                table: "Tecnicos",
                column: "IncentivoId",
                principalTable: "Incentivos",
                principalColumn: "IncentivoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tecnicos_Incentivos_IncentivoId",
                table: "Tecnicos");

            migrationBuilder.AlterColumn<int>(
                name: "IncentivoId",
                table: "Tecnicos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tecnicos_Incentivos_IncentivoId",
                table: "Tecnicos",
                column: "IncentivoId",
                principalTable: "Incentivos",
                principalColumn: "IncentivoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
