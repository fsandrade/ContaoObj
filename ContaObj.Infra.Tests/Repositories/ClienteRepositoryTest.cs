using Bogus;
using ContaObj.Application.Interfaces;
using ContaObj.Domain.Model;
using ContaObj.FakeData.ClienteData;
using ContaObj.FakeData.TelefoneData;
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
    public class ClienteRepositoryTest : IDisposable
    {
        private readonly IClienteRepository repository;
        private readonly ContaObjContext context;
        private readonly Cliente cliente;
        private readonly ClienteFaker clienteFaker;

        public ClienteRepositoryTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContaObjContext>();
            optionsBuilder.UseInMemoryDatabase("MemoryDb");

            context = new ContaObjContext(optionsBuilder.Options);
            repository = new ClienteRepository(context);

            clienteFaker = new ClienteFaker();
            cliente = clienteFaker.Generate();
        }

        private async Task<List<Cliente>> InsertRecords()
        {
            var clientes = clienteFaker.Generate(10);
            foreach (var c in clientes)
            {
                c.Id = 0;
                await context.Clientes.AddAsync(c);
            }
            await context.SaveChangesAsync();
            return clientes;
        }

        [Fact]
        private async Task GetClientesAsync_ComRepositorioPreenchido_DeveHaverRetorno()
        {
            var registros = await InsertRecords();
            var retorno = await repository.GetClientesAsync();

            retorno.Should().HaveCount(registros.Count);
            retorno.First().Telefones.Should().NotBeNull();
        }

        [Fact]
        private async Task GetClientesAsync_RepositorioVazio()
        {
            var retorno = await repository.GetClientesAsync();

            retorno.Should().HaveCount(0);
        }

        [Fact]
        private async Task GetClienteAsync_RetornaClienteCorretamente()
        {
            var registros = await InsertRecords();
            var retorno = await repository.GetClienteAsync(registros.First().Id);

            retorno.Should().BeEquivalentTo(registros.First());
        }

        [Fact]
        private async Task GetClienteAsync_RepositorioVazio_RetornaNulo()
        {
            var retorno = await repository.GetClienteAsync(1);

            retorno.Should().BeNull();
        }

        [Fact]
        private async Task InsertClienteAsync_Sucesso()
        {
            var retorno = await repository.InsertClienteAsync(cliente);

            retorno.Should().BeEquivalentTo(cliente);
        }

        [Fact]
        private async Task InsertClienteAsync_RepositorioVazio_RetornaNulo()
        {
            var retorno = await repository.GetClienteAsync(1);

            retorno.Should().BeNull();
        }

        [Fact]
        private async Task UpdateClienteAsync_Sucesso()
        {
            var registros = await InsertRecords();
            var updatedClient = clienteFaker.Generate();
            var clienteExistente = registros.First();
            updatedClient.Id = clienteExistente.Id;
            updatedClient.Endereco.Id = clienteExistente.Endereco.Id;
            updatedClient.Telefones = clienteExistente.Telefones;

            var retorno = await repository.UpdateClienteAsync(updatedClient);
            retorno.Should().BeEquivalentTo(updatedClient);
        }

        [Fact]
        private async Task UpdateClienteAsync_AdicionaTelefone()
        {
            var registros = await InsertRecords();
            var updatedClient = registros.First();
            var novoTelefone = new TelefoneFaker(0).Generate();
            updatedClient.Telefones.Add(novoTelefone);

            var retorno = await repository.UpdateClienteAsync(updatedClient);
            retorno.Should().BeEquivalentTo(updatedClient);
        }

        [Fact]
        private async Task UpdateClienteAsync_RemoveTelefone()
        {
            var registros = await InsertRecords();
            var updatedClient = registros.First();
            updatedClient.Telefones.Remove(updatedClient.Telefones.First());

            var retorno = await repository.UpdateClienteAsync(updatedClient);

            retorno.Should().BeEquivalentTo(updatedClient);
        }

        [Fact]
        private async Task UpdateClienteAsync_LimpaTelefones()
        {
            var registros = await InsertRecords();
            var updatedClient = registros.First();
            updatedClient.Telefones.Clear();

            var retorno = await repository.UpdateClienteAsync(updatedClient);

            retorno.Should().BeEquivalentTo(updatedClient);
        }

        [Fact]
        private async Task UpdateClienteAsync_RepositorioVazio_RetornaNulo()
        {
            var retorno = await repository.UpdateClienteAsync(cliente);

            retorno.Should().BeNull();
        }

        [Fact]
        private async Task InativarClienteAsync_Sucesso()
        {
            var registros = await InsertRecords();

            var retorno = await repository.InativarClienteAsync(registros.First().Id);

            retorno.Should().BeEquivalentTo(registros.First());
        }

        [Fact]
        private async Task InativarClienteAsync_RepositorioVazio_RetornaNulo()
        {
            var retorno = await repository.InativarClienteAsync(1);

            retorno.Should().BeNull();
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }
    }
}
