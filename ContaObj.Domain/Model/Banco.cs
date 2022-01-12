namespace ContaObj.Domain.Model;

public class Banco
{
    public int Id { get; set; }
    public string Cnpj { get; set; }

    public List<Agencia> Agencias { get; set; }
}