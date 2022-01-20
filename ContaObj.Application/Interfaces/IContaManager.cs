﻿using ContaObj.Domain.ViewModel;
using ContaObj.Domain.ViewModel.Conta;
using ContaObj.Domain.ViewModel.Transacao;

namespace ContaObj.Application.Interfaces;

public interface IContaManager
{
    Task DepositarAsync(OperacaoPropriaConta deposito);

    Task<IEnumerable<NovaTransacao>> ExtratoPorPeriodoAsync(int contaId, DateTime inicio, DateTime fim);

    Task<ContaViewModel> GetContaAsync(int contaId);

    Task<IEnumerable<ContaViewModel>> GetContasAsync();

    Task InativarContaAsync(int contaId);

    Task<ContaViewModel?> InsertContaAsync(NovaConta novaConta);

    Task SacarAsync(OperacaoPropriaConta saque);

    Task AlteraLimiteContaAsync(int contaId, decimal novoLimite);
};