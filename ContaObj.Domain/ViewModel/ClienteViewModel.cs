using ContaObj.Domain.Enumerations;

namespace ContaObj.Domain.ViewModel
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public DateTime DataNascimento { get; set; }
        public char Sexo { get; set; }
        public string Email { get; set; }
        public StatusCliente Status { get; set; }
    }
}
