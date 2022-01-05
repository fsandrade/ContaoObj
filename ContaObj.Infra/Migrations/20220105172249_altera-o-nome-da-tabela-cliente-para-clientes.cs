using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContaObj.Infra.Migrations
{
    public partial class alteraonomedatabelaclienteparaclientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Endereco_EnderecoId",
                table: "Cliente");

            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Cliente_ClienteId",
                table: "Conta");

            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Cliente_ClienteId1",
                table: "Conta");

            migrationBuilder.DropForeignKey(
                name: "FK_Telefone_Cliente_ClienteId",
                table: "Telefone");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacao_Conta_DestinoId",
                table: "Transacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacao_Conta_OrigemId",
                table: "Transacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transacao",
                table: "Transacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.RenameTable(
                name: "Transacao",
                newName: "Transacoe");

            migrationBuilder.RenameTable(
                name: "Cliente",
                newName: "Clientes");

            migrationBuilder.RenameIndex(
                name: "IX_Transacao_OrigemId",
                table: "Transacoe",
                newName: "IX_Transacoe_OrigemId");

            migrationBuilder.RenameIndex(
                name: "IX_Transacao_DestinoId",
                table: "Transacoe",
                newName: "IX_Transacoe_DestinoId");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_EnderecoId",
                table: "Clientes",
                newName: "IX_Clientes_EnderecoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transacoe",
                table: "Transacoe",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Endereco_EnderecoId",
                table: "Clientes",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Clientes_ClienteId",
                table: "Conta",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Clientes_ClienteId1",
                table: "Conta",
                column: "ClienteId1",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Telefone_Clientes_ClienteId",
                table: "Telefone",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoe_Conta_DestinoId",
                table: "Transacoe",
                column: "DestinoId",
                principalTable: "Conta",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoe_Conta_OrigemId",
                table: "Transacoe",
                column: "OrigemId",
                principalTable: "Conta",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Endereco_EnderecoId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Clientes_ClienteId",
                table: "Conta");

            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Clientes_ClienteId1",
                table: "Conta");

            migrationBuilder.DropForeignKey(
                name: "FK_Telefone_Clientes_ClienteId",
                table: "Telefone");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacoe_Conta_DestinoId",
                table: "Transacoe");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacoe_Conta_OrigemId",
                table: "Transacoe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transacoe",
                table: "Transacoe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.RenameTable(
                name: "Transacoe",
                newName: "Transacao");

            migrationBuilder.RenameTable(
                name: "Clientes",
                newName: "Cliente");

            migrationBuilder.RenameIndex(
                name: "IX_Transacoe_OrigemId",
                table: "Transacao",
                newName: "IX_Transacao_OrigemId");

            migrationBuilder.RenameIndex(
                name: "IX_Transacoe_DestinoId",
                table: "Transacao",
                newName: "IX_Transacao_DestinoId");

            migrationBuilder.RenameIndex(
                name: "IX_Clientes_EnderecoId",
                table: "Cliente",
                newName: "IX_Cliente_EnderecoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transacao",
                table: "Transacao",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Endereco_EnderecoId",
                table: "Cliente",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Cliente_ClienteId",
                table: "Conta",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Cliente_ClienteId1",
                table: "Conta",
                column: "ClienteId1",
                principalTable: "Cliente",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Telefone_Cliente_ClienteId",
                table: "Telefone",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacao_Conta_DestinoId",
                table: "Transacao",
                column: "DestinoId",
                principalTable: "Conta",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacao_Conta_OrigemId",
                table: "Transacao",
                column: "OrigemId",
                principalTable: "Conta",
                principalColumn: "Id");
        }
    }
}
