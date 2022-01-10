using Bogus;
using ContaObj.Domain.Model;
using ContaObj.FakeData.ClienteData;

namespace ContaObj.FakeData.EnderecoData
{
    public class TelefoneFaker : Faker<Telefone>
    {
        public TelefoneFaker()
        {
            var id = new Faker().Random.Number(1, 999999);
            var ddd = new Faker().Random.Number(11, 99);
            RuleFor(t => t.Id, f => id);
            RuleFor(t => t.Ddd, f => ddd);
            RuleFor(t => t.Numero, f => f.Phone.PhoneNumber());
        }
    }
}
