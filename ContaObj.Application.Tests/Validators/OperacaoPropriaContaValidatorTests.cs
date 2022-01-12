using ContaObj.Application.Validators;
using ContaObj.Domain.ViewModel.Conta;
using FluentValidation.TestHelper;
using Xunit;

namespace ContaObj.Application.Tests.Validators
{
    public class OperacaoPropriaContaValidatorTests
    {
        private readonly OperacaoPropriaContaValidator validator;

        public OperacaoPropriaContaValidatorTests()
        {
            validator = new OperacaoPropriaContaValidator();
        }

        [Fact]
        public void ShouldNotHaveErrorWhenContaIdIsOk()
        {
            var transacao = new OperacaoPropriaConta { ContaId = 1 };
            var result = validator.TestValidate(transacao);
            result.ShouldNotHaveValidationErrorFor(t => t.ContaId);
        }

        [Fact]
        public void ShouldHaveErrorWhenContaIdIsZero()
        {
            var transacao = new OperacaoPropriaConta { ContaId = 0 };
            var result = validator.TestValidate(transacao);
            result.ShouldHaveValidationErrorFor(t => t.ContaId);
        }

        [Fact]
        public void ShouldHaveErrorWhenContaIdIsNegative()
        {
            var transacao = new OperacaoPropriaConta { ContaId = -1 };
            var result = validator.TestValidate(transacao);
            result.ShouldHaveValidationErrorFor(t => t.ContaId);
        }

        [Fact]
        public void ShouldNotHaveErrorWhenValorTransacaoIsOk()
        {
            var transacao = new OperacaoPropriaConta { ValorTransacao = 10.00M };
            var result = validator.TestValidate(transacao);
            result.ShouldNotHaveValidationErrorFor(t => t.ValorTransacao);
        }

        [Fact]
        public void ShouldHaveErrorWhenValorTransacaoIdIsZero()
        {
            var transacao = new OperacaoPropriaConta { ValorTransacao = 0 };
            var result = validator.TestValidate(transacao);
            result.ShouldHaveValidationErrorFor(t => t.ValorTransacao);
        }

        [Fact]
        public void ShouldHaveErrorWhenValorTransacaoIsNegative()
        {
            var transacao = new OperacaoPropriaConta { ValorTransacao = -1 };
            var result = validator.TestValidate(transacao);
            result.ShouldHaveValidationErrorFor(t => t.ValorTransacao);
        }
    }
}
