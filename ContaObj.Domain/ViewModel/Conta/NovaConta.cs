namespace ContaObj.Domain.ViewModel.Conta;

public class NovaConta
{
    /// <summary>
    /// Limite do cheque especial
    /// </summary>
    /// <example>100.15</example>
    public decimal Limite { get; set; }

    /// <summary>
    /// Id de um cliente existente
    /// </summary>
    /// <example>1</example>
    public int Cliente { get; set; }

    /// <summary>
    /// Id de uma agência existente
    /// </summary>
    /// <example>3</example>
    public int Agencia { get; set; }
}