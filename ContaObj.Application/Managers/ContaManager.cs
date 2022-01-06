using AutoMapper;
using ContaObj.Application.Interfaces;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel;

namespace ContaObj.Application.Managers
{
    public class ContaManager : IContaManager
    {
        private readonly IContaRepository contaRepository;
        private readonly IMapper mapper;

        public ContaManager(IContaRepository contaRepository, IMapper mapper)
        {
            this.contaRepository = contaRepository;
            this.mapper = mapper;
        }

        public Task<bool?> DepositarAsync(int contaId, decimal valorDeposito)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TransacaoViewModel>> ExtratoPorPeriodoAsync(int contaId, DateTime inicio, DateTime fim)
        {
            throw new NotImplementedException();
        }

        public Task<ContaViewModel> GetContaAsync(int contaId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ContaViewModel>> GetContasAsync()
        {
            var contas = await contaRepository.GetContasAsync();
            return mapper.Map<IEnumerable<Conta>, IEnumerable<ContaViewModel>>(contas);
        }

        public async Task<IEnumerable<ContaViewModel>> GetContasPorClienteAsync(int clienteId)
        {
            var contas = await contaRepository.GetContasPorClienteAsync(clienteId);
            return mapper.Map<IEnumerable<Conta>, IEnumerable<ContaViewModel>>(contas);
        }

        public async Task InativarContaAsync(int contaId)
        {
            await contaRepository.InativarContaAsync(contaId);
        }

        public Task<ContaViewModel> InsertContaAsync(NovaConta novaConta)
        {
            throw new NotImplementedException();
        }

        public Task<bool?> SacarAsync(int contaId, decimal valorSaque)
        {
            throw new NotImplementedException();
        }

        public Task<bool?> UpdateContaAsync(AlteraConta alterarConta)
        {
            throw new NotImplementedException();
        }
    }
}
