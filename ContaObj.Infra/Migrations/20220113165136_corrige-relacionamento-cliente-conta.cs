using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContaObj.Infra.Migrations
{
    public partial class corrigerelacionamentoclienteconta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contas_Clientes_ClienteId1",
                table: "Contas");

            migrationBuilder.DropIndex(
                name: "IX_Contas_ClienteId1",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "ClienteId1",
                table: "Contas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteId1",
                table: "Contas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contas_ClienteId1",
                table: "Contas",
                column: "ClienteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Contas_Clientes_ClienteId1",
                table: "Contas",
                column: "ClienteId1",
                principalTable: "Clientes",
                principalColumn: "Id");
        }
    }
}
