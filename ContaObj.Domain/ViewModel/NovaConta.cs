namespace ContaObj.Domain.ViewModel
{
    public class NovaConta
    {
        public decimal Saldo { get; set; }
        public decimal Limite { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public AgenciaViewModel Agencia { get; set; }
    }
}
