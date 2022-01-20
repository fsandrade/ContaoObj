namespace ContaObj.Domain.ViewModel.Transacao;

public class RetornoTransacao
{
    public bool NovaTransacao { get; set; }
    public Model.Transacao Transacao { get; set; }

    public RetornoTransacao(bool novaTransacao, Model.Transacao transacao)
    {
        NovaTransacao = novaTransacao;
        Transacao = transacao;
    }
}