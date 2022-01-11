using Bogus;
using ContaObj.Domain.Model;

namespace ContaObj.FakeData.EnderecoData
{
    public class EnderecoFaker : Faker<Endereco>
    {
        public EnderecoFaker()
        {
            var id = new Faker().Random.Number(1, 999999);
            RuleFor(t => t.Id, f => id);
            RuleFor(t => t.Cep, f => f.Address.ZipCode());
        }
    }
}
