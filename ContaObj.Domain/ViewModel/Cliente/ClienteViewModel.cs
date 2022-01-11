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
        public EnderecoViewModel Endereco { get; set; }
        public List<TelefoneViewModel> Telefones { get; set; }

        public object Clone()
        {
            var cliente = (ClienteViewModel)MemberwiseClone();
            cliente.Endereco = cliente.Endereco.Clone();
            var telefones = new List<TelefoneViewModel>();
            cliente.Telefones.ToList().ForEach(p => telefones.Add(p.Clone()));
            cliente.Telefones = telefones;
            return cliente;
        }

        public ClienteViewModel CloneTipado()
        {
            return (ClienteViewModel)Clone();
        }
    }
}
