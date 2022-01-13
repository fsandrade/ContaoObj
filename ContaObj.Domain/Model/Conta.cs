using ContaObj.Domain.Enumerations;

namespace ContaObj.Domain.Model;

public class Conta
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public decimal Saldo { get; set; }
    public decimal Limite { get; set; }

    public Cliente Cliente { get; set; }
    public Agencia Agencia { get; set; }
    public StatusConta Status { get; set; }

    public void Inativar()
    {
        Status = StatusConta.Inativa;
    }

    public void Sacar(decimal valorSaque)
    {
        Saldo -= valorSaque;
    }

    public void Depositar(decimal valorDeposito)
    {
        Saldo += valorDeposito;
    }

    public void AlterarAgencia(Agencia novaAgencia)
    {
        Agencia = novaAgencia;
    }

    public void AlterarLimite(decimal novoLimite)
    {
        Limite = novoLimite;
    }
}