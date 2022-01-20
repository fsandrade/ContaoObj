using ContaObj.Application.Interfaces;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel.Transacao;
using ContaObj.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ContaObj.Infra.Repositories;

public class TransacaoRepositorio : ITransacaoRepositorio
{
    private readonly ContaObjContext context;

    public TransacaoRepositorio(ContaObjContext context)
    {
        this.context = context;
    }

    public async Task<RetornoTransacao> CriaTransacaoAsync(Transacao transacao)
    {
        var _transacao = await context.Transacoes
            .Include(p => p.Origem)
            .Include(p => p.Destino)
            .FirstOrDefaultAsync(p => p.Id == transacao.Id);
        if (_transacao != null)
        {
            return new RetornoTransacao(false, _transacao);
        }

        var contas = await ValidaContas(transacao);
        ValidaSaldo(contas.Item1, transacao.Valor);
        transacao.Origem = contas.Item1;
        transacao.Destino = contas.Item2;

        await context.Transacoes.AddAsync(transacao);
        await context.SaveChangesAsync();
        return new RetornoTransacao(true, transacao);
    }

    public async Task<Transacao> EfetivaDocAsync(Transacao transacao)
    {
        var _transacao = await context.Transacoes.FindAsync(transacao.Id);
        if (_transacao == null) throw new ApplicationException("Transacao não existente");

        var contas = await ValidaContas(transacao);
        ValidaSaldo(contas.Item1, transacao.Valor);

        _transacao.Origem = contas.Item1;
        _transacao.Destino = contas.Item2;
        _transacao.Efetivar();
        await context.SaveChangesAsync();
        return _transacao;
    }

    private async Task<Tuple<Conta, Conta>> ValidaContas(Transacao transacao)
    {
        var contaOrigem = await context.Contas.FindAsync(transacao.Origem.Id);
        if (contaOrigem == null) throw new ApplicationException("Conta de origem inexistente.");
        var contaDestino = await context.Contas.FindAsync(transacao.Destino.Id);
        if (contaDestino == null) throw new ApplicationException("Conta destino inexistente.");

        return new Tuple<Conta, Conta>(contaOrigem, contaDestino);
    }

    private static void ValidaSaldo(Conta conta, decimal valor)
    {
        if (!conta.PossuiSaldoParaTransacao(valor)) throw new ApplicationException("Saldo insuficiente.");
    }
}