using AutoMapper;
using ContaObj.Application.Interfaces;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel;
using ContaObj.Domain.ViewModel.Conta;
using ContaObj.Domain.ViewModel.Transacao;

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

        public Task DepositarAsync(OperacaoPropriaConta deposito)
        {
            return contaRepository.DepositarAsync(deposito);
        }

        public Task<IEnumerable<NovaTransacao>> ExtratoPorPeriodoAsync(int contaId, DateTime inicio, DateTime fim)
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

        public async Task InativarContaAsync(int contaId)
        {
            await contaRepository.InativarContaAsync(contaId);
        }

        public async Task<ContaViewModel?> InsertContaAsync(NovaConta novaConta)
        {
            var conta = mapper.Map<Conta>(novaConta);
            conta = await contaRepository.InsertContaAsync(conta);
            if (conta is null)
            {
                return null;
            }
            return mapper.Map<ContaViewModel>(conta);
        }

        public Task SacarAsync(OperacaoPropriaConta saque)
        {
            return contaRepository.SacarAsync(saque);
        }

        public Task AlteraLimiteContaAsync(int contaId, decimal novoLimite)
        {
            return contaRepository.AlteraLimiteContaAsync(contaId, novoLimite);
        }
    }
}