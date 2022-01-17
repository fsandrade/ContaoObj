using Bogus;
using ContaObj.Domain.Enumerations;
using ContaObj.Domain.Model;
using ContaObj.FakeData.ContaData;

namespace ContaObj.FakeData.TransacaoData
{
    public class TransacaoFaker : Faker<Transacao>
    {
        public TransacaoFaker()
        {
            var id = new Faker().Random.Number(1, 999999);
            var valor = new Faker().Random.Decimal(1, 99999);
            RuleFor(p => p.Id, f => id);
            RuleFor(p => p.Valor, f => valor);
            RuleFor(p => p.Data, f => DateTime.UtcNow);
            RuleFor(p => p.Origem, f => new ContaFaker().Generate());
            RuleFor(p => p.Destino, f => new ContaFaker().Generate());
            RuleFor(p => p.Tipo, f => f.PickRandom<TipoTransacao>());
            RuleFor(p => p.Status, f => f.PickRandom<StatusTransacao>());
        }

        public TransacaoFaker(Conta contaOrigem, Conta contaDestino)
        {
            var id = new Faker().Random.Number(1, 999999);
            var valor = new Faker().Random.Decimal(1, 99999);
            RuleFor(p => p.Id, f => id);
            RuleFor(p => p.Valor, f => valor);
            RuleFor(p => p.Data, f => DateTime.UtcNow);
            RuleFor(p => p.Origem, f => contaOrigem);
            RuleFor(p => p.Destino, f => contaDestino);
            RuleFor(p => p.Tipo, f => f.PickRandom<TipoTransacao>());
            RuleFor(p => p.Status, f => f.PickRandom<StatusTransacao>());
        }
    }
}
