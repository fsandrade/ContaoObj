using Bogus;
using ContaObj.Domain.Enumerations;
using ContaObj.Domain.Model;
using ContaObj.FakeData.AgenciaData;
using ContaObj.FakeData.ClienteData;

namespace ContaObj.FakeData.ContaData
{
    public class ContaFaker : Faker<Conta>
    {
        public ContaFaker()
        {
            var id = new Faker().Random.Number(1, 999999);
            var saldo = new Faker().Random.Decimal(1, 99999);
            var limite = new Faker().Random.Decimal(1, 99999);
            RuleFor(p => p.Id, f => id);
            RuleFor(p => p.Saldo, f => saldo);
            RuleFor(p => p.Limite, f => limite);
            RuleFor(p => p.Cliente, f => new ClienteFaker().Generate());
            RuleFor(p => p.Agencia, f => new AgenciaFaker().Generate());
            RuleFor(p => p.Status, f => f.PickRandom<StatusConta>());
        }

        public ContaFaker(Cliente cliente, Agencia agencia)
        {
            var id = new Faker().Random.Number(1, 999999);
            var saldo = new Faker().Random.Decimal(1, 99999);
            var limite = new Faker().Random.Decimal(1, 99999);
            RuleFor(p => p.Id, f => id);
            RuleFor(p => p.Saldo, f => saldo);
            RuleFor(p => p.Limite, f => limite);
            RuleFor(p => p.Cliente, f => cliente);
            RuleFor(p => p.Agencia, f => agencia);
            RuleFor(p => p.Status, f => f.PickRandom<StatusConta>());
        }
    }
}
