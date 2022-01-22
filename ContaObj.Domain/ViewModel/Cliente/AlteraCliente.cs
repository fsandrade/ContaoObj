
using ContaObj.Domain.ViewModel.Endereco;

namespace ContaObj.Domain.ViewModel;

public partial class AlteraCliente : NovoCliente
{
    /// <summary>
    /// ID do cliente existente
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }

    public List<AlteraTelefone> Telefones { get; set; }

    public AlteraEndereco Endereco { get; set; }
}
