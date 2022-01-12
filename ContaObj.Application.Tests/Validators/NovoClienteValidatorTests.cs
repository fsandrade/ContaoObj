using ContaObj.Application.Validators;
using ContaObj.Domain.ViewModel;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using Xunit;

namespace ContaObj.Application.Tests.Validators
{
    public class NovoClienteValidatorTests
    {
        private readonly NovoClienteValidator validator;

        public NovoClienteValidatorTests()
        {
            validator = new NovoClienteValidator();
        }

        [Fact]
        public void ShouldNotHaveErrorWhenNomeIsOk()
        {
            var cliente = new NovoCliente { Nome = "João da Silva" };
            var result = validator.TestValidate(cliente);
            result.ShouldNotHaveValidationErrorFor(c => c.Nome);
        }

        [Fact]
        public void ShouldHaveErrorWhenNomeIsNull()
        {
            var cliente = new NovoCliente { Nome = null };
            var result = validator.TestValidate(cliente);
            result.ShouldHaveValidationErrorFor(c => c.Nome);
        }

        [Fact]
        public void ShouldHaveErrorWhenNomeIsEmpty()
        {
            var cliente = new NovoCliente { Nome = "" };
            var result = validator.TestValidate(cliente);
            result.ShouldHaveValidationErrorFor(c => c.Nome);
        }

        [Fact]
        public void ShouldHaveErrorWhenNomeIsBiggerThan100()
        {
            var cliente = new NovoCliente { Nome = "Nome Muito Muito Muito Muito Muito Muito Muito Muito Muito Muito Muito Muito Muito Muito Muito Grande" };
            var result = validator.TestValidate(cliente);
            result.ShouldHaveValidationErrorFor(c => c.Nome);
        }

        [Fact]
        public void ShouldHaveErrorWhenNomeIsLessThan6()
        {
            var cliente = new NovoCliente { Nome = "João" };
            var result = validator.TestValidate(cliente);
            result.ShouldHaveValidationErrorFor(c => c.Nome);
        }

        [Fact]
        public void ShouldNotHaveErrorWhenDocumentoIsOk()
        {
            var cliente = new NovoCliente { Documento = "123456789012" };
            var result = validator.TestValidate(cliente);
            result.ShouldNotHaveValidationErrorFor(c => c.Documento);
        }

        [Fact]
        public void ShouldHaveErrorWhenDocumentoIsNull()
        {
            var cliente = new NovoCliente { Documento = null };
            var result = validator.TestValidate(cliente);
            result.ShouldHaveValidationErrorFor(c => c.Documento);
        }

        [Fact]
        public void ShouldHaveErrorWhenDocumentoIsEmpty()
        {
            var cliente = new NovoCliente { Documento = "" };
            var result = validator.TestValidate(cliente);
            result.ShouldHaveValidationErrorFor(c => c.Documento);
        }

        [Fact]
        public void ShouldHaveErrorWhenDocumentoIsBiggerThan14()
        {
            var cliente = new NovoCliente { Documento = "123456789012345" };
            var result = validator.TestValidate(cliente);
            result.ShouldHaveValidationErrorFor(c => c.Documento);
        }

        [Fact]
        public void ShouldHaveErrorWhenDocumentoIsLessThan9()
        {
            var cliente = new NovoCliente { Documento = "12345678" };
            var result = validator.TestValidate(cliente);
            result.ShouldHaveValidationErrorFor(c => c.Documento);
        }

        [Fact]
        public void ShouldNotHaveErrorWhenDataNascimentoIsOk()
        {
            var cliente = new NovoCliente { DataNascimento = DateTime.Now.AddYears(-20) };
            var result = validator.TestValidate(cliente);
            result.ShouldNotHaveValidationErrorFor(c => c.DataNascimento);
        }

        [Fact]
        public void ShouldHaveErrorWhenDataNascimentoIsNullOrEmpty()
        {
            var cliente = new NovoCliente();
            var result = validator.TestValidate(cliente);
            result.ShouldHaveValidationErrorFor(c => c.DataNascimento);
        }

        [Fact]
        public void ShouldHaveErrorWhenDataNascimentoIsBiggerThan120Years()
        {
            var cliente = new NovoCliente { DataNascimento = DateTime.Now.AddYears(-120).AddDays(-1) };
            var result = validator.TestValidate(cliente);
            result.ShouldHaveValidationErrorFor(c => c.DataNascimento);
        }

        [Fact]
        public void ShouldHaveErrorWhenDataNascimentoIsLessThan18Years()
        {
            var cliente = new NovoCliente { DataNascimento = DateTime.Now.AddYears(-18).AddDays(1) };
            var result = validator.TestValidate(cliente);
            result.ShouldHaveValidationErrorFor(c => c.DataNascimento);
        }

        [Fact]
        public void ShouldHaveErrorWhenTelefonesIsNull()
        {
            var cliente = new NovoCliente { Telefones = null };
            var result = validator.TestValidate(cliente);
            result.ShouldHaveValidationErrorFor(c => c.Telefones);
        }

        [Fact]
        public void ShouldHaveErrorWhenTelefonesIsEmpty()
        {
            var cliente = new NovoCliente { Telefones = new List<NovoTelefone>() };
            var result = validator.TestValidate(cliente);
            result.ShouldHaveValidationErrorFor(c => c.Telefones);
        }

        [Theory]
        [InlineData('M')]
        [InlineData('F')]
        public void ShouldNotHaveErrorWhenSexoIsOk(char sexo)
        {
            var cliente = new NovoCliente { Sexo = sexo };
            var result = validator.TestValidate(cliente);
            result.ShouldNotHaveValidationErrorFor(c => c.Sexo);
        }

        [Fact]
        public void ShouldHaveErrorWhenSexoIsNullOrEmpty()
        {
            var cliente = new NovoCliente();
            var result = validator.TestValidate(cliente);
            result.ShouldHaveValidationErrorFor(c => c.Sexo);
        }
    }
}