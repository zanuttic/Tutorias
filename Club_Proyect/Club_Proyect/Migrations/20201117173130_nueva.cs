using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Club_Proyect.Migrations
{
    public partial class nueva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "horario_DeporteID",
                table: "Socio",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Deporte",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deporte", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "horario_Deporte",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    cantidad_Socios = table.Column<int>(nullable: false),
                    Activo = table.Column<bool>(nullable: false),
                    deporteID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_horario_Deporte", x => x.ID);
                    table.ForeignKey(
                        name: "FK_horario_Deporte_Deporte_deporteID",
                        column: x => x.deporteID,
                        principalTable: "Deporte",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Socio_horario_DeporteID",
                table: "Socio",
                column: "horario_DeporteID");

            migrationBuilder.CreateIndex(
                name: "IX_horario_Deporte_deporteID",
                table: "horario_Deporte",
                column: "deporteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Socio_horario_Deporte_horario_DeporteID",
                table: "Socio",
                column: "horario_DeporteID",
                principalTable: "horario_Deporte",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Socio_horario_Deporte_horario_DeporteID",
                table: "Socio");

            migrationBuilder.DropTable(
                name: "horario_Deporte");

            migrationBuilder.DropTable(
                name: "Deporte");

            migrationBuilder.DropIndex(
                name: "IX_Socio_horario_DeporteID",
                table: "Socio");

            migrationBuilder.DropColumn(
                name: "horario_DeporteID",
                table: "Socio");
        }
    }
}
