using AutoMapper;
using ContaObj.Application.Mappings;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel;
using ContaObj.FakeData.ClienteData;
using FluentAssertions;
using Xunit;

namespace ContaObj.Application.Tests.Mappings;

public class ClienteViewModelMappingProfileTests
{
    private readonly MapperConfiguration config;
    private readonly Cliente cliente;
    private readonly ClienteFaker clienteFaker;

    public ClienteViewModelMappingProfileTests()
    {
        config = new MapperConfiguration(cfg => cfg.AddProfile<ClienteViewModelMappingProfile>());
        clienteFaker = new ClienteFaker();
        cliente = clienteFaker.Generate();
    }

    [Fact]
    public void EnsureThatConfigurationIsValid()
    {
        config.AssertConfigurationIsValid();
    }

    [Fact]
    public void ValidateMappingFromClienteToClienteViewModel()
    {
        var mapper = config.CreateMapper();
        var clienteViewModel = mapper.Map<Cliente, ClienteViewModel>(cliente);
        clienteViewModel.Id.Should().Be(cliente.Id);
        clienteViewModel.Nome.Should().Be(cliente.Nome);
        clienteViewModel.Documento.Should().Be(cliente.Documento);
        clienteViewModel.DataNascimento.Should().Be(cliente.DataNascimento);
        clienteViewModel.Sexo.Should().Be(cliente.Sexo);
        clienteViewModel.Email.Should().Be(cliente.Email);
        clienteViewModel.Status.Should().Be(cliente.Status);
        clienteViewModel.Endereco.Id.Should().Be(cliente.Endereco.Id);
        clienteViewModel.Endereco.Cep.Should().Be(cliente.Endereco.Cep);
        clienteViewModel.Telefones.Should().HaveCount(cliente.Telefones.Count);
        int count = 0;
        foreach (var telefone in cliente.Telefones)
        {
            clienteViewModel.Telefones[count].Id.Should().Be(telefone.Id);
            clienteViewModel.Telefones[count].Ddd.Should().Be(telefone.Ddd);
            clienteViewModel.Telefones[count].Numero.Should().Be(telefone.Numero);
            count++;
        }
        clienteViewModel.Nome.Should().Be(cliente.Nome);
    }
}