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
            return contaRepository.DepositarAsync(contaId, valorDeposito);
        }

        public Task<IEnumerable<TransacaoViewModel>> ExtratoPorPeriodoAsync(int contaId, DateTime inicio, DateTime fim)
        {
            throw new NotImplementedException();
        }

        public async Task<ContaViewModel> GetContaAsync(int contaId)
        {
            var conta = await contaRepository.GetContaAsync(contaId);
            return mapper.Map<ContaViewModel>(conta);
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

        public async Task<ContaViewModel> InsertContaAsync(NovaConta novaConta)
        {
            var conta = mapper.Map<Conta>(novaConta);
            conta = await contaRepository.InsertContaAsync(conta);
            return mapper.Map<ContaViewModel>(conta);
        }

        public Task<bool?> SacarAsync(int contaId, decimal valorSaque)
        {
            return contaRepository.SacarAsync(contaId, valorSaque);
        }

        public async Task<bool?> UpdateContaAsync(AlteraConta alterarConta)
        {
            var conta = mapper.Map<Conta>(alterarConta);
            return await contaRepository.UpdateContaAsync(conta);
        }
    }
}
