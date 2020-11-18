using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Club_Proyect.Migrations
{
    public partial class proyect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "socioID",
                table: "horario_Deporte",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_horario_Deporte_socioID",
                table: "horario_Deporte",
                column: "socioID");

            migrationBuilder.AddForeignKey(
                name: "FK_horario_Deporte_Socio_socioID",
                table: "horario_Deporte",
                column: "socioID",
                principalTable: "Socio",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_horario_Deporte_Socio_socioID",
                table: "horario_Deporte");

            migrationBuilder.DropIndex(
                name: "IX_horario_Deporte_socioID",
                table: "horario_Deporte");

            migrationBuilder.DropColumn(
                name: "socioID",
                table: "horario_Deporte");
        }
    }
}
