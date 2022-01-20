using ContaObj.Domain.Enumerations;

namespace ContaObj.Domain.ViewModel.Transacao;

public class NovaTransacao
{
    /// <summary>
    /// Identificador único gerado pelo consumidor
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Valor da transação
    /// </summary>
    /// <example>15.25</example>
    public decimal Valor { get; set; }

    /// <summary>
    /// Tido da transação
    /// </summary>
    public TipoTransacao Tipo { get; set; }

    /// <summary>
    /// Número identificador da conta de origem
    /// </summary>
    /// <example>1324</example>
    public int Origem { get; set; }

    /// <summary>
    /// Número identificador da conta de destino
    /// </summary>
    /// <example>4321</example>
    public int Destino { get; set; }
}