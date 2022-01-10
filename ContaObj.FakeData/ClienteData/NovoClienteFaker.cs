using Bogus;
using Bogus.Extensions.Brazil;
using ContaObj.Domain.ViewModel;
using ContaObj.FakeData.EnderecoData;
using ContaObj.FakeData.TelefoneData;

namespace ContaObj.FakeData.ClienteData
{
    public class NovoClienteFaker : Faker<NovoCliente>
    {
        public NovoClienteFaker()
        {
            RuleFor(p => p.Nome, f => f.Person.FullName);
            RuleFor(p => p.Sexo, f => f.Person.Gender.ToString().ElementAt(0));
            RuleFor(p => p.Documento, f => f.Person.Cpf());
            RuleFor(p => p.DataNascimento, f => f.Date.Past());
            RuleFor(p => p.Telefones, f => new NovoTelefoneFaker().Generate(3));
            RuleFor(p => p.Endereco, f => new NovoEnderecoFaker().Generate());
        }
    }
}
