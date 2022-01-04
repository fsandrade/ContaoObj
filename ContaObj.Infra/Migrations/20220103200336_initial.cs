using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContaObj.Infra.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cnpj = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Agencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnderecoId = table.Column<int>(type: "int", nullable: false),
                    BancoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agencia_Banco_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Banco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agencia_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnderecoId = table.Column<int>(type: "int", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cliente_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Limite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    AgenciaId = table.Column<int>(type: "int", nullable: false),
                    ClienteId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conta_Agencia_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "Agencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conta_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Conta_Cliente_ClienteId1",
                        column: x => x.ClienteId1,
                        principalTable: "Cliente",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Telefone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ddd = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telefone_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transacao",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrigemId = table.Column<int>(type: "int", nullable: true),
                    DestinoId = table.Column<int>(type: "int", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacao_Conta_DestinoId",
                        column: x => x.DestinoId,
                        principalTable: "Conta",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transacao_Conta_OrigemId",
                        column: x => x.OrigemId,
                        principalTable: "Conta",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agencia_BancoId",
                table: "Agencia",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_Agencia_EnderecoId",
                table: "Agencia",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_EnderecoId",
                table: "Cliente",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Conta_AgenciaId",
                table: "Conta",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Conta_ClienteId",
                table: "Conta",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Conta_ClienteId1",
                table: "Conta",
                column: "ClienteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Telefone_ClienteId",
                table: "Telefone",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_DestinoId",
                table: "Transacao",
                column: "DestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_OrigemId",
                table: "Transacao",
                column: "OrigemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Telefone");

            migrationBuilder.DropTable(
                name: "Transacao");

            migrationBuilder.DropTable(
                name: "Conta");

            migrationBuilder.DropTable(
                name: "Agencia");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Banco");

            migrationBuilder.DropTable(
                name: "Endereco");
        }
    }
}
