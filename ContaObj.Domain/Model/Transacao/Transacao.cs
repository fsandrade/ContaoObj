using ContaObj.Domain.Enumerations;

namespace ContaObj.Domain.Model;

public class Transacao
{
    public long Id { get; set; }
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public Conta? Origem { get; set; }
    public Conta? Destino { get; set; }
    public TipoTransacao Tipo { get; set; }
    public StatusTransacao Status { get; set; }
}