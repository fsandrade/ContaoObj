using Bogus;
using ContaObj.Domain.ViewModel;

namespace ContaObj.FakeData.EnderecoData
{
    public class EnderecoViewModelFaker : Faker<EnderecoViewModel>
    {
        public EnderecoViewModelFaker()
        {
            var id = new Faker().Random.Number(1, 999999);
            RuleFor(t => t.Id, f => id);
            RuleFor(t => t.Cep, f => f.Address.ZipCode());
        }
    }
}
