using ContaObj.Domain.ViewModel.Conta;

namespace ContaObj.Domain.ViewModel
{
    public class AlteraConta: NovaConta
    {
        /// <summary>
        /// Id da conta existente
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
    }
}
