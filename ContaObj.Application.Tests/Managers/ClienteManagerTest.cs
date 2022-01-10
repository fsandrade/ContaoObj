using AutoMapper;
using ContaObj.Application.Interfaces;
using ContaObj.Application.Managers;
using ContaObj.Application.Mappings;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel;
using ContaObj.FakeData.ClienteData;
using ContaObj.Infra.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ContaObj.Application.Tests.Managers
{
    public class ClienteManagerTest
    {
        private readonly IClienteRepository repository;
        private readonly ILogger<ClienteManager> logger;
        private readonly IMapper mapper;
        private readonly IClienteManager manager;
        private readonly Cliente cliente;
        private readonly NovoCliente novoCliente;
        private readonly AlteraCliente alteraCliente;
        private readonly ClienteFaker clienteFaker;
        private readonly NovoClienteFaker novoClienteFaker;
        private readonly AlteraClienteFaker alteraClienteFaker;

        public ClienteManagerTest()
        {
            repository = Substitute.For<IClienteRepository>();
            logger = Substitute.For<ILogger<ClienteManager>>();
            mapper = new MapperConfiguration(p => p.AddProfile<ClienteViewModelMappingProfile>()).CreateMapper();
            manager = new ClienteManager(repository, mapper);

            clienteFaker = new ClienteFaker();
            novoClienteFaker = new NovoClienteFaker();
            alteraClienteFaker = new AlteraClienteFaker();

            cliente = clienteFaker.Generate();
            novoCliente = novoClienteFaker.Generate();
            alteraCliente = alteraClienteFaker.Generate();
        }

        [Fact]
        public async Task GetClientesAsync_Sucesso()
        {
            var listaClientes = clienteFaker.Generate(2);
            repository.GetClientesAsync().Returns(listaClientes);
            var listaClientesMapeados = mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(listaClientes);

            var retorno = await manager.GetClientesAsync();

            await repository.Received().GetClientesAsync();
            retorno.Should().BeEquivalentTo(listaClientesMapeados);
        }

        [Fact]
        public async Task GetClientesAsync_Vazio()
        {
            repository.GetClientesAsync().Returns(new List<Cliente>());

            var retorno = await manager.GetClientesAsync();

            await repository.Received().GetClientesAsync();
            retorno.Should().BeEquivalentTo(new List<Cliente>());
        }

        [Fact]
        public async Task GetClienteAsync_Sucesso()
        {
            repository.GetClienteAsync(Arg.Any<int>()).Returns(cliente);
            var clienteMapeado = mapper.Map<ClienteViewModel>(cliente);
            var retorno = await manager.GetClienteAsync(cliente.Id);

            await repository.Received().GetClienteAsync(Arg.Any<int>());
            retorno.Should().BeEquivalentTo(clienteMapeado);
        }

        [Fact]
        public async Task GetClienteAsync_NaoEncontrado()
        {
            repository.GetClienteAsync(Arg.Any<int>()).Returns(new Cliente());
            var clienteMapeado = mapper.Map<ClienteViewModel>(new Cliente());
            var retorno = await manager.GetClienteAsync(cliente.Id);

            await repository.Received().GetClienteAsync(Arg.Any<int>());
            retorno.Should().BeEquivalentTo(clienteMapeado);
        }

        [Fact]
        public async Task InsertClienteAsync_Sucesso()
        {
            repository.InsertClienteAsync(Arg.Any<Cliente>()).Returns(cliente);
            var controle = mapper.Map<ClienteViewModel>(cliente);
            var retorno = await manager.InsertClienteAsync(novoCliente);

            await repository.Received().InsertClienteAsync(Arg.Any<Cliente>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task UpdateClienteAsync_Sucesso()
        {
            repository.UpdateClienteAsync(Arg.Any<Cliente>()).Returns(cliente);
            var clienteMapeado = mapper.Map<ClienteViewModel>(cliente);
            var retorno = await manager.UpdateClienteAsync(alteraCliente);

            await repository.Received().UpdateClienteAsync(Arg.Any<Cliente>());
            retorno.Should().BeEquivalentTo(clienteMapeado);
        }

        [Fact]
        public async Task UpdateClienteAsync_NaoEncontrado()
        {
            repository.UpdateClienteAsync(Arg.Any<Cliente>()).ReturnsNull();

            var retorno = await manager.UpdateClienteAsync(alteraCliente);

            await repository.Received().UpdateClienteAsync(Arg.Any<Cliente>());
            retorno.Should().BeNull();
        }

        [Fact]
        public async Task InativarClienteAsync_Sucesso()
        {
            repository.InativarClienteAsync(Arg.Any<int>()).Returns(cliente);
            var clienteMapeado = mapper.Map<ClienteViewModel>(cliente);
            var retorno = await manager.InativarClienteAsync(cliente.Id);

            await repository.Received().InativarClienteAsync(Arg.Any<int>());
            retorno.Should().BeEquivalentTo(clienteMapeado);
        }

        [Fact]
        public async Task InativarClienteAsync_NaoEncontrado()
        {
            repository.InativarClienteAsync(Arg.Any<int>()).ReturnsNull();

            var retorno = await manager.InativarClienteAsync(cliente.Id);

            await repository.Received().InativarClienteAsync(Arg.Any<int>());
            retorno.Should().BeNull();
        }
    }
}
