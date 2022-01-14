using Bogus;
using ContaObj.Domain.ViewModel.Conta;

namespace ContaObj.FakeData.ContaData
{
    public class NovaContaFaker : Faker<NovaConta>
    {
        public NovaContaFaker()
        {
            var limite = new Faker().Random.Decimal(1, 99999);
            var clienteId = new Faker().Random.Int(1, 99999);
            var agenciaId = new Faker().Random.Int(1, 99999);
            RuleFor(p => p.Limite, f => limite);
            RuleFor(p => p.Cliente, f => clienteId);
            RuleFor(p => p.Agencia, f => agenciaId);
        }
    }
}
