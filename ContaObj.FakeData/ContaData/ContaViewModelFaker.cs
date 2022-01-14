using Bogus;
using ContaObj.Domain.Enumerations;
using ContaObj.Domain.ViewModel;
using ContaObj.FakeData.AgenciaData;
using ContaObj.FakeData.ClienteData;

namespace ContaObj.FakeData.ContaData
{
    public class ContaViewModelFaker : Faker<ContaViewModel>
    {
        public ContaViewModelFaker()
        {
            var id = new Faker().Random.Number(1, 999999);
            var saldo = new Faker().Random.Decimal(1, 99999);
            var limite = new Faker().Random.Decimal(1, 99999);
            RuleFor(p => p.Id, f => id);
            RuleFor(p => p.Numero, f => id);
            RuleFor(p => p.Saldo, f => saldo);
            RuleFor(p => p.Limite, f => limite);
            RuleFor(p => p.Cliente, f => new ClienteViewModelFaker().Generate());
            RuleFor(p => p.Agencia, f => new AgenciaViewModelFaker().Generate());
            RuleFor(p => p.Status, f => f.PickRandom<StatusConta>());
        }
    }
}
