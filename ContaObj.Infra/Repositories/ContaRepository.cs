using ContaObj.Application.Interfaces;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel.Conta;
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

    public async Task DepositarAsync(OperacaoPropriaConta deposito)
    {
        var contaConsultada = await GetContaAsync(deposito.ContaId);

        if (contaConsultada == null)
        {
            throw new ApplicationException("Conta não encontrada");
        }

        contaConsultada.CreditaSaldo(deposito.Valor);

        //TODO criar nova transação

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Transacao>> ExtratoPorPeriodoAsync(int contaId, DateTime inicio, DateTime fim)
    {
        return await context.Transacoes
            .Where(x => (x.Origem.Id == contaId || x.Destino.Id == contaId) && x.Data.Date >= inicio && x.Data.Date <= fim)
            .ToListAsync();
    }

    public async Task<Conta?> GetContaAsync(int contaId)
    {
        return await context.Contas
            .Include(x => x.Agencia)
            .Include(x => x.Cliente)
            .Where(x => x.Id == contaId)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Conta>> GetContasAsync()
    {
        return await context.Contas
            .Include(p => p.Cliente)
            .Include(p => p.Agencia)
            .Include(p => p.Agencia.Endereco)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task InativarContaAsync(int contaId)
    {
        var contaConsultada = await context.Contas.FindAsync(contaId);
        if (contaConsultada == null)
        {
            throw new ApplicationException("Conta não encontrada");
        }
        contaConsultada.Inativar();
        await context.SaveChangesAsync();
    }

    public async Task<Conta?> InsertContaAsync(Conta novaConta)
    {
        var clienteExistente = await context.Clientes.FindAsync(novaConta.Cliente.Id);

        if (clienteExistente == null)
        {
            return null;
        }

        var agenciaExistente = await context.Agencias.FindAsync(novaConta.Agencia.Id);

        if (agenciaExistente == null)
        {
            return null;
        }

        novaConta.Agencia = agenciaExistente;
        novaConta.Cliente = clienteExistente;

        novaConta.Numero = context.Contas.Any() ? context.Contas.Max(p => p.Numero) + 1 : 1;

        await context.Contas.AddAsync(novaConta);
        await context.SaveChangesAsync();
        return novaConta;
    }

    public async Task SacarAsync(OperacaoPropriaConta saque)
    {
        var contaConsultada = await GetContaAsync(saque.ContaId);

        if (contaConsultada == null)
        {
            throw new ApplicationException("Conta não encontrada");
        }

        if (contaConsultada.Saldo + contaConsultada.Limite < saque.Valor)
        {
            throw new ApplicationException("Saldo insuficiente");
        }

        //TODO criar nova transação
        contaConsultada.DebitaSaldo(saque.Valor);

        await context.SaveChangesAsync();
    }

    public async Task TransferirDeAgenciaAsync(Conta conta, int novaAgenciaId)
    {
        if (conta.Agencia.Id == novaAgenciaId)
        {
            throw new ApplicationException("A conta não pode ser transferida para a mesma agência");
        }

        var contaConsultada = await GetContaAsync(conta.Id);

        if (contaConsultada == null)
        {
            throw new ApplicationException("Conta inexistente");
        }

        var agenciaDestino = await context.Agencias.FindAsync(novaAgenciaId);

        if (agenciaDestino == null)
        {
            throw new ApplicationException("Nova agência inexistente");
        }

        contaConsultada.AlterarAgencia(agenciaDestino);
    }

    public async Task AlteraLimiteContaAsync(int contaId, decimal novoLimite)
    {
        var contaConsultada = await GetContaAsync(contaId);

        if (contaConsultada == null)
        {
            throw new ApplicationException("Conta inexistente");
        }

        contaConsultada.AlterarLimite(novoLimite);

        await context.SaveChangesAsync();
    }
}