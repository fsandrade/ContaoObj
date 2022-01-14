using Bogus;
using Bogus.Extensions.Brazil;
using ContaObj.Domain.Model;

namespace ContaObj.FakeData.BancoData
{
    public class BancoFaker : Faker<Banco>
    {
        public BancoFaker()
        {
            var id = new Faker().Random.Number(1, 999999);
            RuleFor(p => p.Id, f => id);
            RuleFor(p => p.Cnpj, f => f.Company.Cnpj());
        }
    }
}
