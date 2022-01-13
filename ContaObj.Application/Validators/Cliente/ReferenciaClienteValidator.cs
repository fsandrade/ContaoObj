using ContaObj.Application.Interfaces;
using ContaObj.Domain.ViewModel.Cliente;
using FluentValidation;

namespace ContaObj.Application.Validators.Cliente
{
    public class ReferenciaClienteValidator : AbstractValidator<ReferenciaCliente>
    {
        private readonly IClienteRepository repository;

        public ReferenciaClienteValidator(IClienteRepository repository)
        {
            RuleFor(p => p.Id).NotEmpty().NotNull().GreaterThan(0)
                .MustAsync(async (id, cancel) =>
                {
                    return await ExistsOnDatabaseAsync(id);
                }).WithMessage("Cliente não cadastrado");
            this.repository = repository;
        }

        private async Task<bool> ExistsOnDatabaseAsync(int id)
        {
            return await repository.ExistsOnDatabaseAsync(id);
        }
    }
}
