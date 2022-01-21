using AutoMapper;
using ContaObj.Application.Interfaces;
using ContaObj.Application.Managers;
using ContaObj.Application.Mappings;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel.Conta;
using ContaObj.Domain.ViewModel.Transacao;
using ContaObj.FakeData.ContaData;
using ContaObj.FakeData.TransacaoData;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ContaObj.Application.Tests.Managers
{
    public class ContaManagerTest
    {
        private readonly IContaRepository repository;
        private readonly IMapper mapper;
        private readonly IContaManager manager;
        private readonly Conta conta;
        private readonly ContaFaker contaFaker;
        private readonly TransacaoFaker transacaoFaker;
        private readonly OperacaoPropriaContaFaker operacaoPropriaContaFaker;

        public ContaManagerTest()
        {
            repository = Substitute.For<IContaRepository>();
            mapper = new MapperConfiguration(p => p.AddProfile<ContaViewModelMappingProfile>()).CreateMapper();
            manager = new ContaManager(repository, mapper);

            contaFaker = new ContaFaker();
            conta = contaFaker.Generate();
            transacaoFaker = new TransacaoFaker();
            operacaoPropriaContaFaker = new OperacaoPropriaContaFaker(conta.Id);
        }

        [Fact]
        public async Task DepositarAsync_Sucesso()
        {
            var deposito = operacaoPropriaContaFaker.Generate();

            await manager.DepositarAsync(deposito);

            await repository.Received().DepositarAsync(Arg.Any<OperacaoPropriaConta>());
        }

        [Fact]
        public async Task ExtratoPorPeriodoAsync_Sucesso()
        {
            var inicioPeriodo = DateTime.Now;
            var fimPeriodo = DateTime.Now;
            var listaTransacoes = transacaoFaker.Generate(2);
            repository.ExtratoPorPeriodoAsync(conta.Id, inicioPeriodo, fimPeriodo).Returns(listaTransacoes);
            var listaTransacoesMapeadas = mapper.Map<IEnumerable<Transacao>, IEnumerable<TransacaoViewModel>>(listaTransacoes);

            var retorno = await manager.ExtratoPorPeriodoAsync(conta.Id, inicioPeriodo, fimPeriodo);

            await repository.Received().ExtratoPorPeriodoAsync(Arg.Any<int>(), Arg.Any<DateTime>(), Arg.Any<DateTime>());
            retorno.Should().BeEquivalentTo(listaTransacoesMapeadas);
        }

        [Fact]
        public async Task ExtratoPorPeriodoAsync_RepositorioVazio_RetornaListaVazia()
        {
            var inicioPeriodo = DateTime.Now;
            var fimPeriodo = DateTime.Now;
            repository.ExtratoPorPeriodoAsync(conta.Id, inicioPeriodo, fimPeriodo).Returns(new List<Transacao>());

            var retorno = await manager.ExtratoPorPeriodoAsync(conta.Id, inicioPeriodo, fimPeriodo);

            await repository.Received().ExtratoPorPeriodoAsync(Arg.Any<int>(), Arg.Any<DateTime>(), Arg.Any<DateTime>());
            retorno.Count().Should().Be(0);
        }

        [Fact]
        public async Task SacarAsync_Sucesso()
        {
            var saque = operacaoPropriaContaFaker.Generate();

            await manager.SacarAsync(saque);

            await repository.Received().SacarAsync(Arg.Any<OperacaoPropriaConta>());
        }

    }
}
