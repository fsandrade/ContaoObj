using ContaObj.Domain.Enumerations;

namespace ContaObj.Domain.ViewModel.Conta
{
    public class TransacaoConta
    {

        /// <summary>
        /// Valor da transação
        /// </summary>
        /// <example>10.00M</example>
        public decimal ValorTransacao { get; set; }

        /// <summary>
        /// Tipo da transação
        /// </summary>
        /// <example>Saque</example>
        public TipoTransacao TipoTransacao { get; set; }
    }
}
