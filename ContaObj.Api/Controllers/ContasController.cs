using ContaObj.Application.Interfaces;
using ContaObj.Domain.Enumerations;
using ContaObj.Domain.ViewModel;
using ContaObj.Domain.ViewModel.Conta;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContaObj.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContasController : ControllerBase
    {
        private readonly IContaManager contaManager;

        public ContasController(IContaManager contaManager)
        {
            this.contaManager = contaManager;
        }

        [SwaggerOperation(Summary = "Realiza depósito em uma conta")]
        [HttpPut("Depositar")]
        public async Task<ActionResult<bool?>> Depositar([SwaggerParameter("Dados do depósito")] OperacaoPropriaConta deposito)
        {
            if(deposito.TipoTransacao != TipoTransacao.Deposito)
            {
                return BadRequest(
                    new ProblemDetails
                    {
                        Title = "One or more validation errors occurred.",
                        Status = 400,
                        Detail = "O tipo de transação informado é inválido."
                    });
            }

            var depositoRealizado = await contaManager.DepositarAsync(deposito.ContaId, deposito.ValorTransacao);

            if (depositoRealizado == null)
            {
                return NotFound();
            }

            return Ok(depositoRealizado);
        }

        [SwaggerOperation(Summary = "Consulta transações da conta por período")]
        [HttpGet("ExtratoPorPeriodo/contaId={contaId}&inicio={inicio}&fim={fim}")]
        public async Task<ActionResult<IEnumerable<TransacaoViewModel>>> ExtratoPorPeriodo(
            [SwaggerParameter("Id da conta")] int contaId,
            [SwaggerParameter("Início do período")] DateTime inicio,
            [SwaggerParameter("Fim do período")] DateTime fim)
        {
            var transacoesNoPeriodo = await contaManager.ExtratoPorPeriodoAsync(contaId, inicio, fim);

            if (transacoesNoPeriodo == null)
            {
                return NotFound();
            }

            return Ok(transacoesNoPeriodo);
        }

        [SwaggerOperation(Summary = "Consulta conta específica")]
        [HttpGet("{contaId}")]
        public async Task<ActionResult<ContaViewModel>> Get([SwaggerParameter("Id da conta")] int contaId)
        {
            var contaConsultada = await contaManager.GetContaAsync(contaId);

            if (contaConsultada == null)
            {
                return NotFound();
            }

            return Ok(contaConsultada);
        }

        [SwaggerOperation(Summary = "Consulta todas as contas")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContaViewModel>>> Get()
        {
            var contasExistentes = await contaManager.GetContasAsync();

            if (contasExistentes == null)
            {
                return NotFound();
            }

            return Ok(contasExistentes);
        }

        [SwaggerOperation(Summary = "Consulta todas as contas de um cliente")]
        [HttpGet("ContasPorCliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<ContaViewModel>>> ContasPorCliente([SwaggerParameter("Id do cliente")] int clienteId)
        {
            var contasDoCliente = await contaManager.GetContasPorClienteAsync(clienteId);

            if (contasDoCliente == null)
            {
                return NotFound();
            }

            return Ok(contasDoCliente);
        }

        [SwaggerOperation(Summary = "Inativa uma conta específica")]
        [HttpDelete("{contaId}")]
        public async Task<IActionResult> InativarConta([SwaggerParameter("Id da conta")] int contaId)
        {
            await contaManager.InativarContaAsync(contaId);

            return Ok();
        }

        [SwaggerOperation(Summary = "Insere nova conta")]
        [HttpPost]
        public async Task<ActionResult<ContaViewModel>> InsertConta([SwaggerParameter("Conta para inserção")] NovaConta novaConta)
        {
            ContaViewModel contaInserida = await contaManager.InsertContaAsync(novaConta);
            return CreatedAtAction(nameof(Get), new { id = contaInserida.Id }, contaInserida);
        }

        [SwaggerOperation(Summary = "Realiza saque em uma conta")]
        [HttpPut("Sacar")]
        public async Task<ActionResult<bool?>> Sacar([SwaggerParameter("Id da conta")] OperacaoPropriaConta saque)
        {
            if (saque.TipoTransacao != TipoTransacao.Saque)
            {
                return BadRequest(
                    new ProblemDetails
                    {
                        Title = "One or more validation errors occurred.",
                        Status = 400,
                        Detail = "O tipo de transação informado é inválido."
                    });
            }

            var saqueRealizado = await contaManager.SacarAsync(saque.ContaId, saque.ValorTransacao);

            if (saqueRealizado == null)
            {
                return NotFound();
            }

            return Ok(saqueRealizado);
        }

        [SwaggerOperation(Summary = "Altera conta")]
        [HttpPut("{contaId}")]
        public async Task<ActionResult<bool?>> UpdateConta([SwaggerParameter("Id da conta")] int contaId, [SwaggerParameter("Conta para alteração")] AlteraConta alteraConta)
        {
            if (contaId != alteraConta.Id)
            {
                return BadRequest(
                    new ProblemDetails
                    {
                        Title = "One or more validation errors occurred.",
                        Status = 400,
                        Detail = "O Id informado no path é diferente do Id informado no body."
                    });
            }

            var alterouConta = await contaManager.UpdateContaAsync(alteraConta);

            if (alterouConta == null)
            {
                return NotFound();
            }

            return Ok(alterouConta);
        }
    }
}
