using AutoMapper;
using ContaObj.Application.Interfaces;
using ContaObj.Application.Managers;
using ContaObj.Application.Mappings;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel;
using ContaObj.FakeData.AgenciaData;
using ContaObj.FakeData.ClienteData;
using ContaObj.FakeData.ContaData;
using FluentAssertions;
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
        private readonly IMapper mapper;
        private readonly IClienteManager manager;
        private readonly Cliente cliente;
        private readonly ClienteFaker clienteFaker;

        public ClienteManagerTest()
        {
            repository = Substitute.For<IClienteRepository>();
            mapper = new MapperConfiguration(p => p.AddProfile<ClienteViewModelMappingProfile>()).CreateMapper();
            manager = new ClienteManager(repository);

            clienteFaker = new ClienteFaker();

            cliente = clienteFaker.Generate();
        }

        [Fact]
        public async Task GetClientesAsync_Sucesso()
        {
            var agencia = new AgenciaFaker().Generate();
            var listaClientes = clienteFaker.Generate(2);
            List<Cliente> listaClientesClone = new List<Cliente>();
            listaClientes.ForEach(c => {
                c.Contas = new ContaFaker(c, agencia).Generate(2);
                listaClientesClone.Add(c.CloneTipado());
            });
            repository.GetClientesAsync().Returns(listaClientes);

            var retorno = await manager.GetClientesAsync();

            await repository.Received().GetClientesAsync();
            retorno.Should().BeEquivalentTo(listaClientesClone);
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
        public async Task GetClienteAsync_NaoEncontrado_RetornaNulo()
        {
            repository.GetClienteAsync(Arg.Any<int>()).ReturnsNull();

            var retorno = await manager.GetClienteAsync(cliente.Id);

            await repository.Received().GetClienteAsync(Arg.Any<int>());
            retorno.Should().BeNull();
        }

        [Fact]
        public async Task InsertClienteAsync_Sucesso()
        {
            repository.InsertClienteAsync(Arg.Any<Cliente>()).Returns(cliente);
            var controle = mapper.Map<ClienteViewModel>(cliente);
            var retorno = await manager.InsertClienteAsync(cliente);

            await repository.Received().InsertClienteAsync(Arg.Any<Cliente>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task UpdateClienteAsync_Sucesso()
        {
            repository.UpdateClienteAsync(Arg.Any<Cliente>()).Returns(cliente);
            var clienteMapeado = mapper.Map<ClienteViewModel>(cliente);
            var retorno = await manager.UpdateClienteAsync(cliente);

            await repository.Received().UpdateClienteAsync(Arg.Any<Cliente>());
            retorno.Should().BeEquivalentTo(clienteMapeado);
        }

        [Fact]
        public async Task UpdateClienteAsync_NaoEncontrado()
        {
            repository.UpdateClienteAsync(Arg.Any<Cliente>()).ReturnsNull();

            var retorno = await manager.UpdateClienteAsync(cliente);

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
