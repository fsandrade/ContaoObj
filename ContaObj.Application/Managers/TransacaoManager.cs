using AutoMapper;
using ContaObj.Application.Interfaces;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel.Transacao;

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

    public async Task<TransacaoViewModel> TranferirAsync(NovaTransacao transacao)
    {
        var _transacao = mapper.Map<Transacao>(transacao);
        var transacaoRetornada = await repositorio.CriaTransacaoAsync(_transacao);
        if (transacaoRetornada.NovaTransacao)
        {
            producer.EnviaTransacaoParaFila(_transacao);
        }
        return await Task.FromResult(mapper.Map<TransacaoViewModel>(transacaoRetornada.Transacao));
    }
}