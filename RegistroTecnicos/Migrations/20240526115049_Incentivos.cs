using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class Incentivos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tecnicos_TiposTecnicos_TiposTecnicosTipoId",
                table: "Tecnicos");

            migrationBuilder.DropIndex(
                name: "IX_Tecnicos_TiposTecnicosTipoId",
                table: "Tecnicos");

            migrationBuilder.RenameColumn(
                name: "TiposTecnicosTipoId",
                table: "Tecnicos",
                newName: "IncentivoId");

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "TiposTecnicos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Incentivos",
                columns: table => new
                {
                    IncentivoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Fecha = table.Column<string>(type: "TEXT", nullable: false),
                    CantidadServicios = table.Column<int>(type: "INTEGER", nullable: false),
                    Monto = table.Column<decimal>(type: "TEXT", nullable: false),
                    TipoId = table.Column<int>(type: "INTEGER", nullable: false),
                    TecnicoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incentivos", x => x.IncentivoId);
                    table.ForeignKey(
                        name: "FK_Incentivos_TiposTecnicos_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TiposTecnicos",
                        principalColumn: "TipoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tecnicos_idTipo",
                table: "Tecnicos",
                column: "idTipo");

            migrationBuilder.CreateIndex(
                name: "IX_Tecnicos_IncentivoId",
                table: "Tecnicos",
                column: "IncentivoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Incentivos_TipoId",
                table: "Incentivos",
                column: "TipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tecnicos_Incentivos_IncentivoId",
                table: "Tecnicos",
                column: "IncentivoId",
                principalTable: "Incentivos",
                principalColumn: "IncentivoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tecnicos_TiposTecnicos_idTipo",
                table: "Tecnicos",
                column: "idTipo",
                principalTable: "TiposTecnicos",
                principalColumn: "TipoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tecnicos_Incentivos_IncentivoId",
                table: "Tecnicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Tecnicos_TiposTecnicos_idTipo",
                table: "Tecnicos");

            migrationBuilder.DropTable(
                name: "Incentivos");

            migrationBuilder.DropIndex(
                name: "IX_Tecnicos_idTipo",
                table: "Tecnicos");

            migrationBuilder.DropIndex(
                name: "IX_Tecnicos_IncentivoId",
                table: "Tecnicos");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "TiposTecnicos");

            migrationBuilder.RenameColumn(
                name: "IncentivoId",
                table: "Tecnicos",
                newName: "TiposTecnicosTipoId");

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
    }
}
