using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContaObj.Infra.Migrations
{
    public partial class testecrud_cliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Transacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Conta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Cliente",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transacao");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Cliente");
        }
    }
}
