using Bogus;
using ContaObj.Domain.ViewModel;

namespace ContaObj.FakeData.TelefoneData
{
    public class NovoTelefoneFaker : Faker<NovoTelefone>
    {
        public NovoTelefoneFaker()
        {
            var ddd = new Faker().Random.Number(11, 99);
            RuleFor(t => t.Ddd, f => ddd);
            RuleFor(t => t.Numero, f => f.Phone.PhoneNumber());
        }
    }
}
