using Bogus;
using ContaObj.Domain.Model;
using ContaObj.FakeData.BancoData;
using ContaObj.FakeData.ContaData;
using ContaObj.FakeData.EnderecoData;

namespace ContaObj.FakeData.AgenciaData
{
    public class AgenciaFaker : Faker<Agencia>
    {
        public AgenciaFaker()
        {
            var id = new Faker().Random.Number(1, 999999);
            RuleFor(p => p.Id, f => id);
            RuleFor(p => p.Numero, f => id);
            RuleFor(p => p.Nome, f => f.Company.CompanyName());
            RuleFor(p => p.Endereco, f => new EnderecoFaker().Generate());
            RuleFor(p => p.Banco, f => new BancoFaker().Generate());
        }
    }
}
