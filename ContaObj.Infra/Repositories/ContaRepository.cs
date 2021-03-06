
using ContaObj.Application.Interfaces;
using ContaObj.Domain.Model;
using ContaObj.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ContaObj.Infra.Repositories;

public class ContaRepository : IContaRepository
{
    private readonly ContaObjContext context;

    public ContaRepository(ContaObjContext context)
    {
        this.context = context;
    }

    public async Task<bool?> DepositarAsync(int contaId, decimal valorDeposito)
    {
        var contaConsultada = await GetContaAsync(contaId);

        if (contaConsultada == null)
        {
            return null;
        }

        contaConsultada.Depositar(valorDeposito);

        //TODO criar nova transacao

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Transacao>> ExtratoPorPeriodoAsync(int contaId, DateTime inicio, DateTime fim)
    {
        return await context.Transacoes
            .Where(x => (x.Origem.Id == contaId || x.Destino.Id == contaId) && x.Data >= inicio && x.Data <= fim)
            .ToListAsync();
    }

    public async Task<Conta> GetContaAsync(int contaId)
    {
        return await context.Contas
            .FindAsync(contaId);
    }

    public async Task<IEnumerable<Conta>> GetContasAsync()
    {
        return await context.Contas
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Conta>> GetContasPorClienteAsync(int clienteId)
    {
        return await context.Contas
            .Where(x => x.Cliente.Id == clienteId)
            .ToListAsync();
    }

    public async Task InativarContaAsync(int contaId)
    {
        var contaConsultada = await context.Contas.FindAsync(contaId);
        if (contaConsultada == null)
        {
            throw new ApplicationException("Usuário não encontrado");
        }
        contaConsultada.Inativar();
        await context.SaveChangesAsync();

    }

    public async Task<Conta> InsertContaAsync(Conta novaConta)
    {
        await context.Contas.AddAsync(novaConta);
        await context.SaveChangesAsync();
        return novaConta;
    }

    public async Task<bool?> SacarAsync(int contaId, decimal valorSaque)
    {
        var contaConsultada = await GetContaAsync(contaId);

        if (contaConsultada == null)
        {
            return null;
        }

        if (contaConsultada.Saldo + contaConsultada.Limite < valorSaque)
        {
            throw new ApplicationException("Saldo insuficiente");
        }

        //TODO criar nova transação
        contaConsultada.Sacar(valorSaque);

        await context.SaveChangesAsync();
        return true;
    }

    private async Task TransferirDeAgenciaAsync(Conta conta, Conta contaConsultadaParaAlteracao)
    {
        if(conta.Agencia.Id == contaConsultadaParaAlteracao.Agencia.Id)
        {
            return;
        }

        var contaConsultada = await GetContaAsync(conta.Id);
        var agenciaDestino = await context.Agencias.FindAsync(conta.Agencia.Id);

        if (contaConsultada == null || agenciaDestino == null)
        {
            throw new ApplicationException("Nova agência inexistente");
        }

        contaConsultada.AlterarAgencia(agenciaDestino);
    }

    public async Task<bool?> UpdateContaAsync(Conta alterarConta)
    {
        var contaConsultada = await GetContaAsync(alterarConta.Id);

        if (contaConsultada == null)
        {
            return null;
        }

        context.Entry(contaConsultada).CurrentValues.SetValues(alterarConta);
        await TransferirDeAgenciaAsync(alterarConta, contaConsultada);

        await context.SaveChangesAsync();
        return true;
    }
}
