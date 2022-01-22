using AutoMapper;
using ContaObj.Application.Interfaces;
using ContaObj.Domain.Exceptions;
using ContaObj.Domain.Model;

namespace ContaObj.Application.Managers;

public class TransacaoManager : ITransacaoManager
{
    private readonly ITransacaoProducer producer;
    private readonly IMapper mapper;
    private readonly ITransacaoRepositorio repositorio;

    public TransacaoManager(ITransacaoProducer producer, IMapper mapper, ITransacaoRepositorio repositorio)
    {
        this.producer = producer;
        this.mapper = mapper;
        this.repositorio = repositorio;
    }

    public async Task<Transacao> TranferirAsync(Transacao transacao)
    {
        await repositorio.AtribuiContas(transacao);
        ValidaSaldoContaOrigem(transacao);
        Transacao transacaoRetornada;
        try
        {
            transacaoRetornada = await repositorio.CriaTransacaoAsync(transacao);
        }
        catch (TransacaoInvalidaException ex) when (ex.Message == "Transação já existente")
        {
            return await repositorio.ConsultaTransacaoExistenteAscyn(transacao.Id);
        }

        producer.EnviaTransacaoParaFila(transacaoRetornada);

        return await Task.FromResult(transacaoRetornada);
    }

    public async Task<IEnumerable<Transacao>> ConsultaTransacoesAsync()
    {
        return await repositorio.ConsultaTransacoesAsync();
    }

    private static void ValidaSaldoContaOrigem(Transacao transacao)
    {
        if (transacao.Origem == null) throw new TransacaoInvalidaException("Conta origem inválida.");

        if (!transacao.Origem.PossuiSaldoParaTransacao(transacao.Valor))
        {
            throw new TransacaoInvalidaException("Saldo insuficiente.");
        }
    }
}