using ContaObj.Application.Interfaces;
using ContaObj.Domain.Enumerations;
using ContaObj.Domain.ViewModel;
using ContaObj.Domain.ViewModel.Conta;
using ContaObj.Domain.ViewModel.Transacao;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ContaObj.Api.Controllers;

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
    [HttpPost("Depositar")]
    public async Task<IActionResult> Depositar([SwaggerParameter("Dados do depósito")] OperacaoPropriaConta deposito)
    {
        try
        {
            await contaManager.DepositarAsync(deposito);
        }
        catch (ApplicationException ex)
        {
            return NotFound();
        }
        return Ok();
    }

    [SwaggerOperation(Summary = "Consulta transações da conta por período")]
    [HttpGet("ExtratoPorPeriodo")]
    public async Task<ActionResult<IEnumerable<NovaTransacao>>> ExtratoPorPeriodo(
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
        try
        {
            await contaManager.SacarAsync(saque);
        }
        catch (ApplicationException ex)
        {
            return NotFound();
        }
        return Ok();
    }

    [SwaggerOperation(Summary = "Altera limite da conta")]
    [HttpPut("AlteraLimite/{contaId}/{novoLimite}")]
    public async Task<IActionResult> AlteraLimiteConta(
        [SwaggerParameter("Id da conta")] int contaId,
        [SwaggerParameter("Novo valor de limite")] decimal novoLimite)
    {
        try
        {
            await contaManager.AlteraLimiteContaAsync(contaId, novoLimite);
        }
        catch (ApplicationException ex)
        {
            return NotFound();
        }

        return Ok();
    }
}