using ContaObj.Domain.Enumerations;

namespace ContaObj.Domain.Model;

public class Transacao
{
    public Guid Id { get; set; }
    public decimal Valor { get; set; }

    public DateTime Data { get; set; }
    public Conta? Origem { get; set; }
    public Conta? Destino { get; set; }
    public TipoTransacao Tipo { get; set; }
    public StatusTransacao Status { get; set; }

    public void Efetivar()
    {
        if (Origem == null) throw new ApplicationException("Conta de origem precisa ser definida.");
        Origem.DebitaSaldo(Valor);
        if (Destino == null) throw new ApplicationException("Conta de destino precisa ser definida.");
        Destino.CreditaSaldo(Valor);
        Status = StatusTransacao.Concluida;
    }
}