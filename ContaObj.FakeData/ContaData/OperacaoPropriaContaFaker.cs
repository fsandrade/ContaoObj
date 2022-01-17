using Bogus;
using ContaObj.Domain.ViewModel.Conta;

namespace ContaObj.FakeData.ContaData
{
    public class OperacaoPropriaContaFaker : Faker<OperacaoPropriaConta>
    {

        public OperacaoPropriaContaFaker(int contaId)
        {
            var valor = new Faker().Random.Decimal(1, 9999);
            RuleFor(p => p.ContaId, f => contaId);
            RuleFor(p => p.Valor, f => valor);
        }

        public OperacaoPropriaContaFaker(int contaId, decimal valor)
        {
            RuleFor(p => p.ContaId, f => contaId);
            RuleFor(p => p.Valor, f => valor);
        }
    }
}
