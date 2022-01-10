using Bogus;
using ContaObj.Domain.ViewModel;

namespace ContaObj.FakeData.TelefoneData
{
    public class AlteraTelefoneFaker : Faker<AlteraTelefone>
    {
        public AlteraTelefoneFaker()
        {
            var id = new Faker().Random.Number(1, 999999);
            var ddd = new Faker().Random.Number(11, 99);
            RuleFor(t => t.Id, f => id);
            RuleFor(t => t.Ddd, f => ddd);
            RuleFor(t => t.Numero, f => f.Phone.PhoneNumber());
        }
    }
}
