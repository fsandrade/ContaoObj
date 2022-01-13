using ContaObj.Domain.ViewModel.Agencia;
using ContaObj.Domain.ViewModel.Cliente;

namespace ContaObj.Domain.ViewModel
{
    public class NovaConta
    {
        public decimal Saldo { get; set; }
        public decimal Limite { get; set; }
        public ReferenciaCliente Cliente { get; set; }
        public ReferenciaAgencia Agencia { get; set; }
    }
}
