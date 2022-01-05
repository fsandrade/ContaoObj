using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContaObj.Infra.Migrations
{
    public partial class alteraonomedastabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agencia_Banco_BancoId",
                table: "Agencia");

            migrationBuilder.DropForeignKey(
                name: "FK_Agencia_Endereco_EnderecoId",
                table: "Agencia");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Endereco_EnderecoId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Agencia_AgenciaId",
                table: "Conta");

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
                name: "PK_Telefone",
                table: "Telefone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Endereco",
                table: "Endereco");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conta",
                table: "Conta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agencia",
                table: "Agencia");

            migrationBuilder.RenameTable(
                name: "Transacoe",
                newName: "Transacoes");

            migrationBuilder.RenameTable(
                name: "Telefone",
                newName: "Telefones");

            migrationBuilder.RenameTable(
                name: "Endereco",
                newName: "Enderecos");

            migrationBuilder.RenameTable(
                name: "Conta",
                newName: "Contas");

            migrationBuilder.RenameTable(
                name: "Agencia",
                newName: "Agencias");

            migrationBuilder.RenameIndex(
                name: "IX_Transacoe_OrigemId",
                table: "Transacoes",
                newName: "IX_Transacoes_OrigemId");

            migrationBuilder.RenameIndex(
                name: "IX_Transacoe_DestinoId",
                table: "Transacoes",
                newName: "IX_Transacoes_DestinoId");

            migrationBuilder.RenameIndex(
                name: "IX_Telefone_ClienteId",
                table: "Telefones",
                newName: "IX_Telefones_ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Conta_ClienteId1",
                table: "Contas",
                newName: "IX_Contas_ClienteId1");

            migrationBuilder.RenameIndex(
                name: "IX_Conta_ClienteId",
                table: "Contas",
                newName: "IX_Contas_ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Conta_AgenciaId",
                table: "Contas",
                newName: "IX_Contas_AgenciaId");

            migrationBuilder.RenameIndex(
                name: "IX_Agencia_EnderecoId",
                table: "Agencias",
                newName: "IX_Agencias_EnderecoId");

            migrationBuilder.RenameIndex(
                name: "IX_Agencia_BancoId",
                table: "Agencias",
                newName: "IX_Agencias_BancoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transacoes",
                table: "Transacoes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Telefones",
                table: "Telefones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enderecos",
                table: "Enderecos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contas",
                table: "Contas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agencias",
                table: "Agencias",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agencias_Banco_BancoId",
                table: "Agencias",
                column: "BancoId",
                principalTable: "Banco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agencias_Enderecos_EnderecoId",
                table: "Agencias",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Enderecos_EnderecoId",
                table: "Clientes",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contas_Agencias_AgenciaId",
                table: "Contas",
                column: "AgenciaId",
                principalTable: "Agencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contas_Clientes_ClienteId",
                table: "Contas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contas_Clientes_ClienteId1",
                table: "Contas",
                column: "ClienteId1",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Telefones_Clientes_ClienteId",
                table: "Telefones",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Contas_DestinoId",
                table: "Transacoes",
                column: "DestinoId",
                principalTable: "Contas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Contas_OrigemId",
                table: "Transacoes",
                column: "OrigemId",
                principalTable: "Contas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agencias_Banco_BancoId",
                table: "Agencias");

            migrationBuilder.DropForeignKey(
                name: "FK_Agencias_Enderecos_EnderecoId",
                table: "Agencias");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Enderecos_EnderecoId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Contas_Agencias_AgenciaId",
                table: "Contas");

            migrationBuilder.DropForeignKey(
                name: "FK_Contas_Clientes_ClienteId",
                table: "Contas");

            migrationBuilder.DropForeignKey(
                name: "FK_Contas_Clientes_ClienteId1",
                table: "Contas");

            migrationBuilder.DropForeignKey(
                name: "FK_Telefones_Clientes_ClienteId",
                table: "Telefones");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Contas_DestinoId",
                table: "Transacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Contas_OrigemId",
                table: "Transacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transacoes",
                table: "Transacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Telefones",
                table: "Telefones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enderecos",
                table: "Enderecos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contas",
                table: "Contas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agencias",
                table: "Agencias");

            migrationBuilder.RenameTable(
                name: "Transacoes",
                newName: "Transacoe");

            migrationBuilder.RenameTable(
                name: "Telefones",
                newName: "Telefone");

            migrationBuilder.RenameTable(
                name: "Enderecos",
                newName: "Endereco");

            migrationBuilder.RenameTable(
                name: "Contas",
                newName: "Conta");

            migrationBuilder.RenameTable(
                name: "Agencias",
                newName: "Agencia");

            migrationBuilder.RenameIndex(
                name: "IX_Transacoes_OrigemId",
                table: "Transacoe",
                newName: "IX_Transacoe_OrigemId");

            migrationBuilder.RenameIndex(
                name: "IX_Transacoes_DestinoId",
                table: "Transacoe",
                newName: "IX_Transacoe_DestinoId");

            migrationBuilder.RenameIndex(
                name: "IX_Telefones_ClienteId",
                table: "Telefone",
                newName: "IX_Telefone_ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Contas_ClienteId1",
                table: "Conta",
                newName: "IX_Conta_ClienteId1");

            migrationBuilder.RenameIndex(
                name: "IX_Contas_ClienteId",
                table: "Conta",
                newName: "IX_Conta_ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Contas_AgenciaId",
                table: "Conta",
                newName: "IX_Conta_AgenciaId");

            migrationBuilder.RenameIndex(
                name: "IX_Agencias_EnderecoId",
                table: "Agencia",
                newName: "IX_Agencia_EnderecoId");

            migrationBuilder.RenameIndex(
                name: "IX_Agencias_BancoId",
                table: "Agencia",
                newName: "IX_Agencia_BancoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transacoe",
                table: "Transacoe",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Telefone",
                table: "Telefone",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Endereco",
                table: "Endereco",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conta",
                table: "Conta",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agencia",
                table: "Agencia",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agencia_Banco_BancoId",
                table: "Agencia",
                column: "BancoId",
                principalTable: "Banco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agencia_Endereco_EnderecoId",
                table: "Agencia",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Endereco_EnderecoId",
                table: "Clientes",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Agencia_AgenciaId",
                table: "Conta",
                column: "AgenciaId",
                principalTable: "Agencia",
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
    }
}
