namespace ContaObj.Domain.Model;

public class Telefone
{
    public int Id { get; set; }
    public int Ddd { get; set; }
    public string Numero { get; set; }

    public Cliente Cliente { get; set; }
}