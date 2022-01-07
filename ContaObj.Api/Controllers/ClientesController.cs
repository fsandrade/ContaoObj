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

        /// <summary>
        /// Retorna todos os clientes
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteViewModel>>> GetNome()
        {
            var clientes = await clienteManager.GetClientesAsync();

            if (clientes.Any())
            {
                return Ok(clientes);
            }

            return NotFound();
        }

        /// <summary>
        /// Retorna cliente pelo id
        /// </summary>
        /// <param name="id">Id do cliente</param>
        [HttpGet("{id}")]      
        public async Task<ActionResult<ClienteViewModel>> GetNome(int id)
        {
            var cliente = await clienteManager.GetClienteAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        /// <summary>
        /// Insere novo cliente
        /// </summary>
        /// <param name="novoCliente">Cliente para inserção</param>
        [HttpPost]      
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status417ExpectationFailed)]
        public async Task<ActionResult<ClienteViewModel>> PostNome(NovoCliente novoCliente)
        {
            ClienteViewModel clienteInserido = await clienteManager.InsertClienteAsync(novoCliente);
            return CreatedAtAction(nameof(GetNome), new { id = clienteInserido.Id }, clienteInserido);
        }

        /// <summary>
        /// Altera cliente existente
        /// </summary>
        /// <param name="id">Id do cliente</param>
        /// <param name="alteraCliente">Cliente a ser alterado</param>
        [HttpPut("{id}")]      
        public async Task<ActionResult<AlteraCliente>> PutNome(int id, AlteraCliente alteraCliente)
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

            if(clienteAlterado == null)
            {
                return NotFound();
            }

            return Ok(clienteAlterado);
        }

        /// <summary>
        /// Inativa cliente
        /// </summary>
        /// <param name="clienteId">Id do cliente a ser inativado</param>
        [HttpDelete("{clienteId}")]
        public async Task<IActionResult> DeleteNome(int clienteId)
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