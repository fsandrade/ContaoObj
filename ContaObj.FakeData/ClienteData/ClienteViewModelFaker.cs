﻿using Bogus;
using Bogus.Extensions.Brazil;
using ContaObj.Domain.ViewModel;
using ContaObj.FakeData.EnderecoData;
using ContaObj.FakeData.TelefoneData;

namespace ContaObj.FakeData.ClienteData
{
    public class ClienteViewModelFaker : Faker<ClienteViewModel>
    {
        public ClienteViewModelFaker()
        {
            var id = new Faker().Random.Number(1, 999999);
            RuleFor(p => p.Id, f => id);
            RuleFor(p => p.DataNascimento, f => f.Date.Past());
            RuleFor(p => p.Nome, f => f.Person.FullName);
            RuleFor(p => p.Sexo, f => f.Person.Gender.ToString().ElementAt(0));
            RuleFor(p => p.Documento, f => f.Person.Cpf());
            RuleFor(p => p.Telefones, f => new TelefoneViewModelFaker().Generate(2));
            RuleFor(p => p.Endereco, f => new EnderecoViewModelFaker().Generate());
        }
    }
}