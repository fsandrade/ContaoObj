using Bogus;
using ContaObj.Domain.ViewModel;

namespace ContaObj.FakeData.EnderecoData
{
    public class NovoEnderecoFaker : Faker<NovoEndereco>
    {
        public NovoEnderecoFaker()
        {
            RuleFor(t => t.Cep, f => f.Address.ZipCode());
        }
    }
}
