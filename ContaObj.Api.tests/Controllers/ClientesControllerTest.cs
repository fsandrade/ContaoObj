using ContaObj.Api.Controllers;
using ContaObj.Application.Interfaces;
using ContaObj.Domain.ViewModel;
using ContaObj.FakeData.ClienteData;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ContaObj.Api.tests.Controllers
{
    public class ClientesControllerTest
    {
        private readonly IClienteManager clienteManager;
        private readonly ClientesController controller;
        private readonly ClienteViewModel clienteViewModel;
        private readonly List<ClienteViewModel> clientesViewModel;
        private readonly NovoCliente novoCliente;
        private readonly AlteraCliente alteraCliente;

        public ClientesControllerTest()
        {
            clienteManager = Substitute.For<IClienteManager>();
            controller = new ClientesController(clienteManager);

            clienteViewModel = new ClienteViewModelFaker().Generate();
            clientesViewModel = new ClienteViewModelFaker().Generate(10);
            novoCliente = new NovoClienteFaker().Generate();
            alteraCliente = new AlteraClienteFaker().Generate();
        }

        [Fact]
        public async Task Get_Ok()
        {
            var listaCliente = new List<ClienteViewModel>();
            clientesViewModel.ForEach(p => listaCliente.Add(p.CloneTipado()));

            clienteManager.GetClientesAsync().Returns(clientesViewModel);

            var resultado = await controller.Get();

            await clienteManager.Received().GetClientesAsync();
            resultado.Result.Should().BeOfType(typeof(OkObjectResult));
            ((ObjectResult)resultado.Result).Value.Should().BeEquivalentTo(clientesViewModel);
        }

        [Fact]
        public async Task Get_NotFound()
        {
            clienteManager.GetClientesAsync().Returns(new List<ClienteViewModel>());

            var resultado = await controller.Get();

            await clienteManager.Received().GetClientesAsync();
            resultado.Result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        public async Task GetById_Ok()
        {
            clienteManager.GetClienteAsync(Arg.Any<int>()).Returns(clienteViewModel.CloneTipado());

            var resultado = await controller.Get(clienteViewModel.Id);

            await clienteManager.Received().GetClienteAsync(Arg.Any<int>());
            ((ObjectResult)resultado.Result).Value.Should().BeEquivalentTo(clienteViewModel);
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
            clienteManager.InsertClienteAsync(Arg.Any<NovoCliente>()).Returns(clienteViewModel.CloneTipado());

            var resultado = await controller.Post(novoCliente);

            await clienteManager.Received().InsertClienteAsync(Arg.Any<NovoCliente>());
            resultado.Result.Should().BeOfType(typeof(CreatedAtActionResult));
            ((ObjectResult)resultado.Result).Value.Should().BeEquivalentTo(clienteViewModel);
        }

        [Fact]
        public async Task Put_Ok()
        {
            clienteManager.UpdateClienteAsync(Arg.Any<AlteraCliente>()).Returns(clienteViewModel.CloneTipado());

            var resultado = await controller.Put(alteraCliente.Id, alteraCliente);

            await clienteManager.Received().UpdateClienteAsync(Arg.Any<AlteraCliente>());
            resultado.Result.Should().BeOfType(typeof(OkObjectResult));
            ((ObjectResult)resultado.Result).Value.Should().BeEquivalentTo(clienteViewModel);
        }

        [Fact]
        public async Task Put_BadRequest()
        {
            clienteManager.UpdateClienteAsync(Arg.Any<AlteraCliente>()).ReturnsNull();

            var resultado = await controller.Put(alteraCliente.Id + 1, alteraCliente);

            resultado.Result.Should().BeOfType(typeof(BadRequestObjectResult));
        }

        [Fact]
        public async Task Put_NotFound()
        {
            clienteManager.UpdateClienteAsync(Arg.Any<AlteraCliente>()).ReturnsNull();

            var resultado = await controller.Put(alteraCliente.Id, alteraCliente);

            await clienteManager.Received().UpdateClienteAsync(Arg.Any<AlteraCliente>());
            resultado.Result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        public async Task Delete_NoContent()
        {
            clienteManager.InativarClienteAsync(Arg.Any<int>()).Returns(clienteViewModel);

            var resultado = await controller.Delete(clienteViewModel.Id);

            await clienteManager.Received().InativarClienteAsync(Arg.Any<int>());
            resultado.Should().BeOfType(typeof(NoContentResult));
        }

        [Fact]
        public async Task Delete_NotFound()
        {
            clienteManager.InativarClienteAsync(clienteViewModel.Id).ReturnsNull();

            var resultado = await controller.Delete(clienteViewModel.Id);

            await clienteManager.Received().InativarClienteAsync(Arg.Any<int>());
            resultado.Should().BeOfType(typeof(NotFoundResult));
        }
    }
}
