namespace ContaObj.Domain.ViewModel
{
    public class EnderecoViewModel
    {
        public int Id { get; set; }
        public string Cep { get; set; }

        public EnderecoViewModel Clone()
        {
            return (EnderecoViewModel)MemberwiseClone();
        }
    }
}