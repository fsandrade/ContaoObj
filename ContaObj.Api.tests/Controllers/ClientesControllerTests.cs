using AutoMapper;
using ContaObj.Api.Controllers;
using ContaObj.Application.Interfaces;
using ContaObj.Application.Mappings;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel;
using ContaObj.FakeData.ClienteData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ContaObj.Api.tests.Controllers;

public class ClientesControllerTests
{
    private readonly IClienteManager clienteManager;
    private readonly IMapper mapper;
    private readonly ClientesController controller;
    private readonly Cliente cliente;
    private readonly List<Cliente> clientes;
    private readonly NovoCliente novoCliente;
    private readonly AlteraCliente alteraCliente;

    public ClientesControllerTests()
    {
        clienteManager = Substitute.For<IClienteManager>();
        mapper = new MapperConfiguration(p => p.AddProfile<ClienteViewModelMappingProfile>()).CreateMapper();
        controller = new ClientesController(clienteManager, mapper);

        cliente = new ClienteFaker().Generate();
        clientes = new ClienteFaker().Generate(10);
        novoCliente = new NovoClienteFaker().Generate();
        alteraCliente = new AlteraClienteFaker().Generate();
    }

    [Fact]
    public async Task Get_Ok()
    {
        clienteManager.GetClientesAsync().Returns(clientes);
        var clientesMapeados = mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(clientes);

        var resultado = await controller.Get();

        await clienteManager.Received().GetClientesAsync();
        resultado.Result.Should().BeOfType(typeof(OkObjectResult));
        ((ObjectResult)resultado.Result).Value.Should().BeEquivalentTo(clientesMapeados);
    }

    [Fact]
    public async Task Get_NotFound()
    {
        clienteManager.GetClientesAsync().Returns(new List<Cliente>());

        var resultado = await controller.Get();

        await clienteManager.Received().GetClientesAsync();
        resultado.Result.Should().BeOfType(typeof(NotFoundResult));
    }

    [Fact]
    public async Task GetById_Ok()
    {
        var clienteMapeado = mapper.Map<ClienteViewModel>(cliente);
        clienteManager.GetClienteAsync(Arg.Any<int>()).Returns(cliente);

        var resultado = await controller.Get(cliente.Id);

        await clienteManager.Received().GetClienteAsync(Arg.Any<int>());
        ((ObjectResult)resultado.Result).Value.Should().BeEquivalentTo(clienteMapeado);
        resultado.Result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Fact]
    public async Task GetById_NotFound()
    {
        clienteManager.GetClienteAsync(Arg.Any<int>()).ReturnsNull();

        var resultado = await controller.Get(1);

        await clienteManager.Received().GetClienteAsync(Arg.Any<int>());
        resultado.Result.Should().BeOfType(typeof(NotFoundResult));
    }

    [Fact]
    public async Task Post_Created()
    {
        var clienteMapeado = mapper.Map<ClienteViewModel>(cliente);
        clienteManager.InsertClienteAsync(Arg.Any<Cliente>()).Returns(cliente);

        var resultado = await controller.Post(novoCliente);

        await clienteManager.Received().InsertClienteAsync(Arg.Any<Cliente>());
        resultado.Result.Should().BeOfType(typeof(CreatedAtActionResult));
        ((ObjectResult)resultado.Result).Value.Should().BeEquivalentTo(clienteMapeado);
    }

    [Fact]
    public async Task Put_Ok()
    {
        clienteManager.UpdateClienteAsync(Arg.Any<Cliente>()).Returns(cliente);

        var resultado = await controller.Put(alteraCliente.Id, alteraCliente);

        await clienteManager.Received().UpdateClienteAsync(Arg.Any<Cliente>());
        resultado.Result.Should().BeOfType(typeof(OkObjectResult));
        ((ObjectResult)resultado.Result).Value.Should().BeEquivalentTo(cliente);
    }

    [Fact]
    public async Task Put_BadRequest()
    {
        clienteManager.UpdateClienteAsync(Arg.Any<Cliente>()).ReturnsNull();

        var resultado = await controller.Put(alteraCliente.Id + 1, alteraCliente);

        resultado.Result.Should().BeOfType(typeof(BadRequestObjectResult));
    }

    [Fact]
    public async Task Put_NotFound()
    {
        clienteManager.UpdateClienteAsync(Arg.Any<Cliente>()).ReturnsNull();

        var resultado = await controller.Put(alteraCliente.Id, alteraCliente);

        await clienteManager.Received().UpdateClienteAsync(Arg.Any<Cliente>());
        resultado.Result.Should().BeOfType(typeof(NotFoundResult));
    }

    [Fact]
    public async Task Delete_NoContent()
    {
        clienteManager.InativarClienteAsync(Arg.Any<int>()).Returns(cliente);

        var resultado = await controller.Delete(cliente.Id);

        await clienteManager.Received().InativarClienteAsync(Arg.Any<int>());
        resultado.Should().BeOfType(typeof(NoContentResult));
    }

    [Fact]
    public async Task Delete_NotFound()
    {
        clienteManager.InativarClienteAsync(cliente.Id).ReturnsNull();

        var resultado = await controller.Delete(cliente.Id);

        await clienteManager.Received().InativarClienteAsync(Arg.Any<int>());
        resultado.Should().BeOfType(typeof(NotFoundResult));
    }
}