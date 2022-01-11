using Bogus;
using ContaObj.Domain.Model;

namespace ContaObj.FakeData.TelefoneData
{
    public class TelefoneFaker : Faker<Telefone>
    {
        public TelefoneFaker(int? idParametro = null)
        {
            var id = new Faker().Random.Int(1, 999999);
            var ddd = new Faker().Random.Number(11, 99);
            RuleFor(t => t.Id, f => idParametro ?? id);
            RuleFor(t => t.Ddd, f => ddd);
            RuleFor(t => t.Numero, f => f.Phone.PhoneNumber());
        }
    }
}
