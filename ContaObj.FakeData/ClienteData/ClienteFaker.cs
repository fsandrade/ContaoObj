using Bogus;
using Bogus.Extensions.Brazil;
using ContaObj.Domain.Model;
using ContaObj.FakeData.ContaData;
using ContaObj.FakeData.EnderecoData;
using ContaObj.FakeData.TelefoneData;

namespace ContaObj.FakeData.ClienteData
{
    public class ClienteFaker : Faker<Cliente>
    {
        public ClienteFaker()
        {
            var id = new Faker().Random.Number(1, 999999);
            RuleFor(p => p.Id, f => id);
            RuleFor(p => p.DataNascimento, f => f.Date.Past());
            RuleFor(p => p.Email, f => f.Internet.Email());
            RuleFor(p => p.Nome, f => f.Person.FullName);
            RuleFor(p => p.Sexo, f => f.Person.Gender.ToString().ElementAt(0));
            RuleFor(p => p.Documento, f => f.Person.Cpf());
            RuleFor(p => p.Telefones, f => GenerateUniqueIdPhoneNumbers(3));
            RuleFor(p => p.Endereco, f => new EnderecoFaker().Generate());
        }

        private List<Telefone> GenerateUniqueIdPhoneNumbers(int quantity)
        {
            List<Telefone> phonenumbers = new List<Telefone>();
            for(int i = 0; i < quantity; i++)
            {
                phonenumbers.Add(new TelefoneFaker().Generate());
            }
            return phonenumbers;
        }
    }
}
