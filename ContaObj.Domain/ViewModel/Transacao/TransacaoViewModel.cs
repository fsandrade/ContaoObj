using ContaObj.Domain.Enumerations;

namespace ContaObj.Domain.ViewModel.Transacao;

public class TransacaoViewModel
{
    public Guid Id { get; set; }
    public decimal Valor { get; set; }

    public DateTime Data { get; set; }
    public int? Origem { get; set; }
    public int? Destino { get; set; }
    public TipoTransacao Tipo { get; set; }
    public StatusTransacao Status { get; set; }
}