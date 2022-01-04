namespace ContaObj.Domain.ViewModel;

public class NovoCliente
{
    public string Nome { get; set; }
    public string Documento { get; set; }
    public DateTime DataNascimento { get; set; }
    public char Sexo { get; set; }
    public string Email { get; set; }
    public NovoEndereco Endereco { get; set; }
    public List<NovoTelefone> Telefones { get; set; }
}