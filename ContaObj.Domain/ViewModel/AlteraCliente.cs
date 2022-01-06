
namespace ContaObj.Domain.ViewModel;

public partial class AlteraCliente : NovoCliente
{
    public int Id { get; set; }

    public List<AlteraTelefone> Telefones { get; set; }
}
