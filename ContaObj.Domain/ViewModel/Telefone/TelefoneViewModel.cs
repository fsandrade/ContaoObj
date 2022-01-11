namespace ContaObj.Domain.ViewModel
{
    public class TelefoneViewModel
    {
        public int Id { get; set; }
        public int Ddd { get; set; }
        public string Numero { get; set; }

        public TelefoneViewModel Clone()
        {
            return (TelefoneViewModel)MemberwiseClone();
        }
    }
}