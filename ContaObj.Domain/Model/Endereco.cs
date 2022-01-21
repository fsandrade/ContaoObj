namespace ContaObj.Domain.Model;

public class Endereco
{
    public int Id { get; set; }
    public string Cep { get; set; }

    public void AlteraEndereco(Endereco endereco)
    {
        Id = endereco.Id;
        Cep = endereco.Cep; 
    }

    public Endereco Clone()
    {
        return (Endereco)MemberwiseClone();
    }
}