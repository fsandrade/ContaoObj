using ContaObj.Domain.Enumerations;

namespace ContaObj.Domain.Model;

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Documento { get; set; }
    public DateTime DataNascimento { get; set; }
    public char Sexo { get; set; }
    public string Email { get; set; }
    public Endereco Endereco { get; set; }
    public List<Telefone> Telefones { get; set; }
    public StatusCliente Status { get; set; }
    public List<Conta> Contas { get; set; }

    public void Inativar()
    {       
        Status = StatusCliente.Inativo;
    }

    public void AtualizaTelefones(List<Telefone> telefones)
    {
        Telefones = telefones;
    }
}