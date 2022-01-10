using ContaObj.Application.Interfaces;
using ContaObj.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ContaObj.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase
{
    private readonly IClienteManager clienteManager;

    public ClientesController(IClienteManager clienteManager)
    {
        this.clienteManager = clienteManager;
    }

    [SwaggerOperation(Summary = "Retorna todos os clientes")]
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

    [SwaggerOperation(Summary = "Retorna cliente pelo id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteViewModel>> Get([SwaggerParameter("Id do cliente")] int id)
    {
        var cliente = await clienteManager.GetClienteAsync(id);

        if (cliente == null)
        {
            return NotFound();
        }

        return Ok(cliente);
    }

    [SwaggerOperation(Summary = "Insere novo cliente")]
    [HttpPost]
    public async Task<ActionResult<ClienteViewModel>> Post([SwaggerParameter("Cliente para inserção")] NovoCliente novoCliente)
    {
        ClienteViewModel clienteInserido = await clienteManager.InsertClienteAsync(novoCliente);
        return CreatedAtAction(nameof(Get), new { id = clienteInserido.Id }, clienteInserido);
    }

    [SwaggerOperation(Summary = "Altera cliente existente")]
    [HttpPut("{id}")]
    public async Task<ActionResult<AlteraCliente>> Put([SwaggerParameter("Id do cliente")] int id, AlteraCliente alteraCliente)
    {
        if (id != alteraCliente.Id)
        {
            return BadRequest(
                new ProblemDetails
                {
                    Title = "One or more validation errors occurred.",
                    Status = 400,
                    Detail = "O Id informado no path é diferente do Id informado no body."
                });
        }
        var clienteAlterado = await clienteManager.UpdateClienteAsync(alteraCliente);

        if (clienteAlterado == null)
        {
            return NotFound();
        }

        return Ok(clienteAlterado);
    }

    [SwaggerOperation(Summary = "Inativa cliente")]
    [HttpDelete("{clienteId}")]
    public async Task<IActionResult> Delete([SwaggerParameter("Id do cliente")] int clienteId)
    {
        var clienteInativado = await clienteManager.InativarClienteAsync(clienteId);

        if (clienteInativado == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}
