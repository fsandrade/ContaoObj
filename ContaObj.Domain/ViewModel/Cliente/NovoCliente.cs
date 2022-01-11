namespace ContaObj.Domain.ViewModel;

public class NovoCliente
{
    /// <example>Marlon</example>
    public string Nome { get; set; }

    /// <summary>
    /// Cpf ou Rg ou CNH do cliente
    /// </summary>
    /// <example>12544875862</example>
    public string Documento { get; set; }

    /// <example>1995-05-30</example>
    public DateTime DataNascimento { get; set; }

    /// <example>M</example>
    public char Sexo { get; set; }

    /// <example>marlon@teste.com</example>
    public string Email { get; set; }

    public NovoEndereco Endereco { get; set; }
    public List<NovoTelefone> Telefones { get; set; }
}