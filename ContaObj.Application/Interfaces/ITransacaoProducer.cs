using ContaObj.Domain.Model;

namespace ContaObj.Application.Interfaces;

public interface ITransacaoProducer
{
    void EnviaTransacaoParaFila(Transacao transacao);
}