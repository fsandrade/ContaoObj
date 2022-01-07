﻿using ContaObj.Application.Interfaces;
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
        [ProducesResponseType(typeof(ClienteViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ClienteViewModel>>> Get()
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
        /// <param name="id">id do cliente</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClienteViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClienteViewModel>> Get(int id)
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
        [ProducesResponseType(typeof(ClienteViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClienteViewModel>> Post(NovoCliente novoCliente)
        {
            ClienteViewModel clienteInserido = await clienteManager.InsertClienteAsync(novoCliente);
            return CreatedAtAction(nameof(Get), new { id = clienteInserido.Id }, clienteInserido);
        }

        /// <summary>
        /// Altera cliente existente
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <param name="alteraCliente">cliente a ser alterado</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ClienteViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Inativa cliente
        /// </summary>
        /// <param name="clienteId">id do cliente a ser inativado</param>
        [HttpDelete("{clienteId}")]
        [ProducesResponseType(typeof(ClienteViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
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