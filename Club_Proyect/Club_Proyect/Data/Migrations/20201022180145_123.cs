using Microsoft.EntityFrameworkCore.Migrations;

namespace Club_Proyect.Data.Migrations
{
    public partial class _123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clientes_personas_personaID",
                table: "clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_empleados_personas_personaID",
                table: "empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_socios_personas_personaID",
                table: "socios");

            migrationBuilder.DropForeignKey(
                name: "FK_vecinos_personas_personaID",
                table: "vecinos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_vecinos",
                table: "vecinos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_socios",
                table: "socios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_personas",
                table: "personas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_empleados",
                table: "empleados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clientes",
                table: "clientes");

            migrationBuilder.RenameTable(
                name: "vecinos",
                newName: "Vecino");

            migrationBuilder.RenameTable(
                name: "socios",
                newName: "Socio");

            migrationBuilder.RenameTable(
                name: "personas",
                newName: "Persona");

            migrationBuilder.RenameTable(
                name: "empleados",
                newName: "Empleado");

            migrationBuilder.RenameTable(
                name: "clientes",
                newName: "Cliente");

            migrationBuilder.RenameIndex(
                name: "IX_vecinos_personaID",
                table: "Vecino",
                newName: "IX_Vecino_personaID");

            migrationBuilder.RenameIndex(
                name: "IX_socios_personaID",
                table: "Socio",
                newName: "IX_Socio_personaID");

            migrationBuilder.RenameIndex(
                name: "IX_empleados_personaID",
                table: "Empleado",
                newName: "IX_Empleado_personaID");

            migrationBuilder.RenameIndex(
                name: "IX_clientes_personaID",
                table: "Cliente",
                newName: "IX_Cliente_personaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vecino",
                table: "Vecino",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Socio",
                table: "Socio",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persona",
                table: "Persona",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Persona_personaID",
                table: "Cliente",
                column: "personaID",
                principalTable: "Persona",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Empleado_Persona_personaID",
                table: "Empleado",
                column: "personaID",
                principalTable: "Persona",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Socio_Persona_personaID",
                table: "Socio",
                column: "personaID",
                principalTable: "Persona",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vecino_Persona_personaID",
                table: "Vecino",
                column: "personaID",
                principalTable: "Persona",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Persona_personaID",
                table: "Cliente");

            migrationBuilder.DropForeignKey(
                name: "FK_Empleado_Persona_personaID",
                table: "Empleado");

            migrationBuilder.DropForeignKey(
                name: "FK_Socio_Persona_personaID",
                table: "Socio");

            migrationBuilder.DropForeignKey(
                name: "FK_Vecino_Persona_personaID",
                table: "Vecino");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vecino",
                table: "Vecino");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Socio",
                table: "Socio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persona",
                table: "Persona");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.RenameTable(
                name: "Vecino",
                newName: "vecinos");

            migrationBuilder.RenameTable(
                name: "Socio",
                newName: "socios");

            migrationBuilder.RenameTable(
                name: "Persona",
                newName: "personas");

            migrationBuilder.RenameTable(
                name: "Empleado",
                newName: "empleados");

            migrationBuilder.RenameTable(
                name: "Cliente",
                newName: "clientes");

            migrationBuilder.RenameIndex(
                name: "IX_Vecino_personaID",
                table: "vecinos",
                newName: "IX_vecinos_personaID");

            migrationBuilder.RenameIndex(
                name: "IX_Socio_personaID",
                table: "socios",
                newName: "IX_socios_personaID");

            migrationBuilder.RenameIndex(
                name: "IX_Empleado_personaID",
                table: "empleados",
                newName: "IX_empleados_personaID");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_personaID",
                table: "clientes",
                newName: "IX_clientes_personaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_vecinos",
                table: "vecinos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_socios",
                table: "socios",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_personas",
                table: "personas",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_empleados",
                table: "empleados",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clientes",
                table: "clientes",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_clientes_personas_personaID",
                table: "clientes",
                column: "personaID",
                principalTable: "personas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_empleados_personas_personaID",
                table: "empleados",
                column: "personaID",
                principalTable: "personas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_socios_personas_personaID",
                table: "socios",
                column: "personaID",
                principalTable: "personas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vecinos_personas_personaID",
                table: "vecinos",
                column: "personaID",
                principalTable: "personas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
