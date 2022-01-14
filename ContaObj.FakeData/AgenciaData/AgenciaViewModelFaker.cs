using Bogus;
using ContaObj.Domain.ViewModel.Agencia;
using ContaObj.FakeData.EnderecoData;

namespace ContaObj.FakeData.AgenciaData
{
    public class AgenciaViewModelFaker : Faker<AgenciaViewModel>
    {
        public AgenciaViewModelFaker()
        {
            var numero = new Faker().Random.Number(1, 999999);
            RuleFor(p => p.Numero, f => numero);
            RuleFor(p => p.Nome, f => f.Company.CompanyName());
            RuleFor(p => p.Endereco, f => new EnderecoViewModelFaker().Generate());
        }
    }
}
