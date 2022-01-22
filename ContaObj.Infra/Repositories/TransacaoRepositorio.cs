using ContaObj.Application.Interfaces;
using ContaObj.Domain.Exceptions;
using ContaObj.Domain.Model;
using ContaObj.Infra.Database;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ContaObj.Infra.Repositories;

public class TransacaoRepositorio : ITransacaoRepositorio
{
    private readonly ContaObjContext context;
    private readonly IConfiguration configuration;

    public TransacaoRepositorio(ContaObjContext context, IConfiguration configuration)
    {
        this.context = context;
        this.configuration = configuration;
    }

    private IDbConnection CriaConexao()
    {
        return new SqlConnection(configuration.GetConnectionString("ContaObjDb"));
    }

    public async Task<IEnumerable<Transacao>> ConsultaTransacoesAsync()
    {
        var conexao = CriaConexao();
        const string sql = "SELECT * FROM dbo.Transacoes";
        return await conexao.QueryAsync<Transacao>(sql);
    }

    public async Task<IEnumerable<Transacao>> ConsultaTransacoesAsync(int IdConta)
    {
        var conexao = CriaConexao();
        const string sql = "SELECT * FROM dbo.Transacoes WHERE OrigemId = @IdConta OR DestinoId = @IdConta";
        var parametros = new { IdConta = IdConta };
        return await conexao.QueryAsync<Transacao>(sql, parametros);
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