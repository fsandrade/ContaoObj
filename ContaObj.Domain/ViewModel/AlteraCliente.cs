
namespace ContaObj.Domain.ViewModel;

public partial class AlteraCliente : NovoCliente
{
    /// <summary>
    /// ID do cliente existente
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Lista de possíveis telefones alterados
    /// </summary>
    public List<AlteraTelefone> Telefones { get; set; }
}
