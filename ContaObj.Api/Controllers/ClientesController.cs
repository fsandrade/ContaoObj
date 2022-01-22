using AutoMapper;
using ContaObj.Application.Interfaces;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ContaObj.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase
{
    private readonly IClienteManager clienteManager;
    private readonly IMapper mapper;

    public ClientesController(IClienteManager clienteManager, IMapper mapper)
    {
        this.clienteManager = clienteManager;
        this.mapper = mapper;
    }

    [SwaggerOperation(Summary = "Retorna todos os clientes")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteViewModel>>> Get()
    {
        var clientes = await clienteManager.GetClientesAsync();

        if (!clientes.Any())
        {
            return NotFound();
        }

        return Ok(mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(clientes));
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

        return Ok(mapper.Map<ClienteViewModel>(cliente));
    }

    [SwaggerOperation(Summary = "Insere novo cliente")]
    [HttpPost]
    public async Task<ActionResult<ClienteViewModel>> Post([SwaggerParameter("Cliente para inserção")] NovoCliente novoCliente)
    {
        var cliente = mapper.Map<Cliente>(novoCliente);

        ClienteViewModel clienteInserido = mapper.Map<ClienteViewModel>(await clienteManager.InsertClienteAsync(cliente));

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

        var cliente = mapper.Map<Cliente>(alteraCliente);

        var clienteAlterado = await clienteManager.UpdateClienteAsync(cliente);

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

    [SwaggerOperation(Summary = "Consulta todas as contas de um cliente")]
    [HttpGet("ContasPorCliente/{clienteId}")]
    public async Task<ActionResult<IEnumerable<ContaViewModel>>> ContasPorCliente([SwaggerParameter("Id do cliente")] int clienteId)
    {
        var contasDoCliente = await clienteManager.GetContasPorClienteAsync(clienteId);

        if (contasDoCliente == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<IEnumerable<Conta>, IEnumerable<ContaViewModel>>(contasDoCliente));
    }
}