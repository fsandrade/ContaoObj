using ContaObj.Domain.Enumerations;

namespace ContaObj.Domain.ViewModel.Conta
{
    public class TransacaoConta
    {
        /// <summary>
        /// Valor da transação
        /// </summary>
        /// <example>10.00M</example>
        public decimal Valor { get; set; }

        public bool PossuiValorValido()
        {
            return Valor > 0;
        }
    }
}