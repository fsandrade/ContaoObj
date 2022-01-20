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
            var id = Guid.NewGuid();
            var valor = new Faker().Random.Decimal(1, 99999);
            RuleFor(p => p.Id, id);
            RuleFor(p => p.Valor, valor);
            RuleFor(p => p.Data, DateTime.UtcNow);
            RuleFor(p => p.Origem, new ContaFaker().Generate());
            RuleFor(p => p.Destino, new ContaFaker().Generate());
            RuleFor(p => p.Tipo, f => f.PickRandom<TipoTransacao>());
            RuleFor(p => p.Status, f => f.PickRandom<StatusTransacao>());
        }

        public TransacaoFaker(Conta contaOrigem, Conta contaDestino)
        {
            var id = Guid.NewGuid();
            var valor = new Faker().Random.Decimal(1, 99999);
            RuleFor(p => p.Id, id);
            RuleFor(p => p.Valor, valor);
            RuleFor(p => p.Data, DateTime.UtcNow);
            RuleFor(p => p.Origem, contaOrigem);
            RuleFor(p => p.Destino, contaDestino);
            RuleFor(p => p.Tipo, f => f.PickRandom<TipoTransacao>());
            RuleFor(p => p.Status, f => f.PickRandom<StatusTransacao>());
        }
    }
}