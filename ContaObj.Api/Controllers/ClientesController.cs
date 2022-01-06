using ContaObj.Application.Interfaces;
using ContaObj.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ContaObj.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteManager clienteManager;

        public ClientesController(IClienteManager clienteManager)
        {
            this.clienteManager = clienteManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteViewModel>>> Get()
        {
            var clientes = await clienteManager.GetClientesAsync();

            if (clientes.Any())
            {
                return Ok(clientes);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteViewModel>> Get(int id)
        {
            var cliente = await clienteManager.GetClienteAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteViewModel>> Post(NovoCliente novoCliente)
        {
            ClienteViewModel clienteInserido = await clienteManager.InsertClienteAsync(novoCliente);
            return CreatedAtAction(nameof(Get), new { id = clienteInserido.Id }, clienteInserido);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AlteraCliente>> Put(int id, AlteraCliente alteraCliente)
        {
            if (id != alteraCliente.Id)
            {
                return BadRequest();
            }
            var clienteAlterado = await clienteManager.UpdateClienteAsync(alteraCliente);

            if(clienteAlterado == null)
            {
                return NotFound();
            }

            return Ok(clienteAlterado);
        }



        [HttpDelete("{clienteId}")]
        public async Task<IActionResult> Delete(int clienteId)
        {
            var inativouCliente = await clienteManager.InativarClienteAsync(clienteId);

            if(inativouCliente == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}