using ContaObj.Application.Interfaces;
using ContaObj.Domain.Enumerations;
using ContaObj.Domain.Model;
using ContaObj.FakeData.AgenciaData;
using ContaObj.FakeData.ClienteData;
using ContaObj.FakeData.ContaData;
using ContaObj.FakeData.TransacaoData;
using ContaObj.Infra.Database;
using ContaObj.Infra.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ContaObj.Infra.Tests.Repositories
{
    public class ContaRepositoryTest : IDisposable
    {
        private readonly IContaRepository repository;
        private readonly ContaObjContext context;
        private readonly ContaFaker contaFaker;
        private readonly AgenciaFaker agenciaFaker;
        private readonly ClienteFaker clienteFaker;

        public ContaRepositoryTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContaObjContext>();
            optionsBuilder.UseInMemoryDatabase("MemoryDb");

            context = new ContaObjContext(optionsBuilder.Options);
            repository = new ContaRepository(context);

            contaFaker = new ContaFaker();
            agenciaFaker = new AgenciaFaker();
            clienteFaker = new ClienteFaker();
        }

        public async Task<List<Conta>> InsereContas()
        {
            var contas = contaFaker.Generate(5);
            foreach (var c in contas)
            {
                c.Id = 0;
                await context.Contas.AddAsync(c);
            }
            await context.SaveChangesAsync();
            return contas;
        }

        public async Task<List<Agencia>> InsereAgencias()
        {
            var agencias = agenciaFaker.Generate(2);
            foreach (var a in agencias)
            {
                a.Id = 0;
                await context.Agencias.AddAsync(a);
            }
            await context.SaveChangesAsync();
            return agencias;
        }

        public async Task<List<Cliente>> InsereClientes()
        {
            var clientes = clienteFaker.Generate(2);
            foreach (var c in clientes)
            {
                c.Id = 0;
                await context.Clientes.AddAsync(c);
            }
            await context.SaveChangesAsync();
            return clientes;
        }

        public async Task<List<Transacao>> InsereTransacoes(Conta contaOrigem, Conta contaDestino, int quantidade)
        {
            var transacoes = new TransacaoFaker(contaOrigem, contaDestino).Generate(quantidade);
            foreach (var t in transacoes)
            {
                t.Id = Guid.NewGuid();
                await context.Transacoes.AddAsync(t);
            }
            await context.SaveChangesAsync();
            return transacoes;
        }

        [Fact]
        public async Task InsertContaAsync_Sucesso()
        {
            var agencias = await InsereAgencias();
            var clientes = await InsereClientes();
            var conta = new ContaFaker(clientes.First(), agencias.First()).Generate();
            var contaClone = conta.Clone();
            contaClone.Numero = 1;

            var retorno = await repository.InsertContaAsync(conta);

            retorno.Should().BeEquivalentTo(contaClone);
        }

        [Fact]
        public async Task InsertContaAsync_RepositorioVazio_RetornaNulo()
        {
            var retorno = await repository.GetContaAsync(1);

            retorno.Should().BeNull();
        }

        [Fact]
        public async Task GetContasAsync_ComRepositorioPreenchido_DeveHaverRetorno()
        {
            var registros = await InsereContas();
            var retorno = await repository.GetContasAsync();

            retorno.Should().HaveCount(registros.Count);
        }

        [Fact]
        public async Task GetContasAsync_RepositorioVazio()
        {
            var retorno = await repository.GetContasAsync();

            retorno.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetContaAsync_RetornaContaCorretamente()
        {
            var registros = await InsereContas();
            var retorno = await repository.GetContaAsync(registros.First().Id);

            retorno.Should().BeEquivalentTo(registros.First());
        }

        [Fact]
        public async Task GetContaAsync_RepositorioVazio_RetornaNulo()
        {
            var retorno = await repository.GetContaAsync(1);

            retorno.Should().BeNull();
        }

        [Fact]
        public async Task InativarContaAsync_Sucesso()
        {
            var registros = await InsereContas();

            await repository.InativarContaAsync(registros.First().Id);

            registros.First().Status.Should().Be(StatusConta.Inativa);
        }

        [Fact]
        public async Task InativarContaAsync_ContaInexistente_LancaExcecao()
        {
            Func<Task> inativaConta = async () => await repository.InativarContaAsync(1);

            await inativaConta
                .Should()
                .ThrowAsync<ApplicationException>()
                .WithMessage("Conta não encontrada");
        }

        [Fact]
        public async Task DepositarAsync_Sucesso()
        {
            var registros = await InsereContas();
            var saldoInicial = registros.First().Saldo;
            var deposito = new OperacaoPropriaContaFaker(registros.First().Id).Generate();

            await repository.DepositarAsync(deposito);

            //TODO testar se transação foi criada com sucesso

            registros.First().Saldo.Should().Be(saldoInicial + deposito.Valor);
        }

        [Fact]
        public async Task DepositarAsync_ContaInexistente_LancaExcecao()
        {
            var deposito = new OperacaoPropriaContaFaker(1).Generate();

            Func<Task> depositoRealizado = async () => await repository.DepositarAsync(deposito);

            await depositoRealizado
            .Should()
            .ThrowAsync<ApplicationException>()
            .WithMessage("Conta não encontrada");
        }

        [Fact]
        public async Task SacarAsync_Sucesso()
        {
            var registros = await InsereContas();
            var saldoInicial = registros.First().Saldo;
            var valorValidoParaSaque = saldoInicial / 2;
            var valorSaldoVerificacao = saldoInicial - valorValidoParaSaque;
            var saque = new OperacaoPropriaContaFaker(registros.First().Id, valorValidoParaSaque).Generate();

            await repository.SacarAsync(saque);

            //TODO testar se transação foi criada com sucesso

            registros.First().Saldo.Should().Be(valorSaldoVerificacao);
        }

        [Fact]
        public async Task SacarAsync_ContaInexistente_LancaExcecao()
        {
            var saque = new OperacaoPropriaContaFaker(1).Generate();

            Func<Task> saqueRealizado = async () => await repository.SacarAsync(saque);

            await saqueRealizado
            .Should()
            .ThrowAsync<ApplicationException>()
            .WithMessage("Conta não encontrada");
        }

        [Fact]
        public async Task SacarAsync_ComSaldoInsuficiente_LancaExcecao()
        {
            var registros = await InsereContas();
            var saldoInicial = registros.First().Saldo;
            var limiteInicial = registros.First().Limite;
            var valorInvalidoParaSaque = saldoInicial + limiteInicial + 5000;
            var saque = new OperacaoPropriaContaFaker(registros.First().Id, valorInvalidoParaSaque).Generate();

            Func<Task> saqueRealizado = async () => await repository.SacarAsync(saque);

            await saqueRealizado
            .Should()
            .ThrowAsync<ApplicationException>()
            .WithMessage("Saldo insuficiente");
        }

        [Fact]
        public async Task AlteraLimiteContaAsync_Sucesso()
        {
            var registros = await InsereContas();
            var novoLimite = 1000;

            await repository.AlteraLimiteContaAsync(registros.First().Id, novoLimite);

            registros.First().Limite.Should().Be(novoLimite);
        }

        [Fact]
        public async Task AlteraLimiteContaAsync_ContaInexistente_LancaExcecao()
        {
            var novoLimite = 1000;

            Func<Task> limiteAlterado = async () => await repository.AlteraLimiteContaAsync(1, novoLimite); ;

            await limiteAlterado
            .Should()
            .ThrowAsync<ApplicationException>()
            .WithMessage("Conta inexistente");
        }

        [Fact]
        public async Task TransferirDeAgenciaAsync_Sucesso()
        {
            var registros = await InsereContas();
            var agencias = await InsereAgencias();

            await repository.TransferirDeAgenciaAsync(registros.First(), agencias.Last().Id);

            registros.First().Agencia.Id.Should().Be(agencias.Last().Id);
        }

        [Fact]
        public async Task TransferirDeAgenciaAsync_MesmaAgencia_LancaExcecao()
        {
            var registros = await InsereContas();

            Func<Task> transferiuAgencia = async () => await repository.TransferirDeAgenciaAsync(registros.First(), registros.First().Agencia.Id);

            await transferiuAgencia
            .Should()
            .ThrowAsync<ApplicationException>()
            .WithMessage("A conta não pode ser transferida para a mesma agência");
        }

        [Fact]
        public async Task TransferirDeAgenciaAsync_ContaInexistente_LancaExcecao()
        {
            var agencias = await InsereAgencias();

            Func<Task> transferiuAgencia = async () => await repository.TransferirDeAgenciaAsync(contaFaker.Generate(), agencias.First().Id);

            await transferiuAgencia
            .Should()
            .ThrowAsync<ApplicationException>()
            .WithMessage("Conta inexistente");
        }

        [Fact]
        public async Task TransferirDeAgenciaAsync_AgenciaInexistente_LancaExcecao()
        {
            var registros = await InsereContas();

            Func<Task> transferiuAgencia = async () => await repository.TransferirDeAgenciaAsync(registros.First(), agenciaFaker.Generate().Id);

            await transferiuAgencia
            .Should()
            .ThrowAsync<ApplicationException>()
            .WithMessage("Nova agência inexistente");
        }

        [Fact]
        public async Task ExtratoPorPeriodoAsync_RepositorioPreenchido_RetornaTransacoes()
        {
            var registros = await InsereContas();
            var dataAtual = DateTime.Now.Date;
            await InsereTransacoes(registros.First(), registros.Last(), 2);

            var transacoesRegistradas = await repository.ExtratoPorPeriodoAsync(registros.First().Id, dataAtual, dataAtual);

            transacoesRegistradas.Should().NotBeNull();
            transacoesRegistradas.Count().Should().Be(2);
        }

        [Fact]
        public async Task ExtratoPorPeriodoAsync_RepositorioVazio_RetornaListaVazia()
        {
            var registros = await InsereContas();
            var dataAtual = DateTime.Now.Date;

            var transacoesRegistradas = await repository.ExtratoPorPeriodoAsync(registros.First().Id, dataAtual, dataAtual);

            transacoesRegistradas.Count().Should().Be(0);
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }
    }
}