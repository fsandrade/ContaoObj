using ContaObj.Domain.Enumerations;
using ContaObj.Domain.ViewModel.Agencia;

namespace ContaObj.Domain.ViewModel
{
    public class ContaViewModel
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public decimal Saldo { get; set; }
        public decimal Limite { get; set; }

        public ClienteViewModel Cliente { get; set; }
        public AgenciaViewModel Agencia { get; set; }
        public StatusConta Status { get; set; }
    }
}
