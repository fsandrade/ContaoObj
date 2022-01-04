namespace ContaObj.Domain.Model;

public class Agencia
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public string Nome { get; set; }
    public Endereco Endereco { get; set; }

    public List<Conta> Contas { get; set; }

    public Banco Banco { get; set; }
}