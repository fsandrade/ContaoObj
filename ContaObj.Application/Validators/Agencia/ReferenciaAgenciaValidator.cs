using ContaObj.Application.Interfaces;
using ContaObj.Domain.ViewModel.Agencia;
using FluentValidation;

namespace ContaObj.Application.Validators.Agencia
{
    public class ReferenciaAgenciaValidator : AbstractValidator<ReferenciaAgencia>
    {
        private readonly IAgenciaRepository repository;

        public ReferenciaAgenciaValidator(IAgenciaRepository repository)
        {
            RuleFor(p => p.Id).NotEmpty().NotNull().GreaterThan(0)
                .MustAsync(async (id, cancel) =>
                {
                    return await ExistsOnDatabaseAsync(id);
                }).WithMessage("Agência não cadastrada");
            this.repository = repository;
        }

        private async Task<bool> ExistsOnDatabaseAsync(int id)
        {
            return await repository.ExistsOnDatabaseAsync(id);
        }
    }
}
