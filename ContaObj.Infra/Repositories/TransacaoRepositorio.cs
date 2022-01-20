using ContaObj.Application.Interfaces;
using ContaObj.Domain.Exceptions;
using ContaObj.Domain.Model;
using ContaObj.Infra.Database;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ContaObj.Infra.Repositories;

public class TransacaoRepositorio : ITransacaoRepositorio
{
    private readonly ContaObjContext context;

    public TransacaoRepositorio(ContaObjContext context)
    {
        this.context = context;
    }

    public async Task<Transacao> CriaTransacaoAsync(Transacao transacao)
    {
        try
        {
            await context.Transacoes.AddAsync(transacao);
            await context.SaveChangesAsync();
            return transacao;
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException?.InnerException is SqlException innerException && (innerException.Number == 2627 || innerException.Number == 2601))
            {
                throw new TransacaoInvalidaException("Transação já existente");
            }
            throw;
        }
    }

    public async Task<Transacao> EfetivaDocAsync(Transacao transacao)
    {
        var _transacao = await context.Transacoes
                                    .Include(p => p.Origem)
                                    .Include(p => p.Destino)
                                    .FirstOrDefaultAsync(p => p.Id == transacao.Id);
        if (_transacao == null) throw new TransacaoInvalidaException("Transação não existente");
        //ValidaSaldoContaOrigem(_transacao);
        _transacao.Efetivar();
        await context.SaveChangesAsync();
        return _transacao;
    }

    public async Task AtribuiContas(Transacao transacao)
    {
        transacao.Origem = await context.Contas.FindAsync(transacao?.Origem?.Id);
        transacao.Destino = await context.Contas.FindAsync(transacao?.Destino?.Id);
    }

    public async Task<Transacao> ConsultaTransacaoExistenteAscyn(Guid id)
    {
        return await context.Transacoes
                            .Include(p => p.Origem)
                            .Include(p => p.Destino)
                            .FirstAsync(p => p.Id == id);
    }
}