﻿using AutoMapper;
using ContaObj.Domain.Enumerations;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel;
using ContaObj.Domain.ViewModel.Cliente;

namespace ContaObj.Application.Mappings;

public class ClienteViewModelMappingProfile : Profile
{
    public ClienteViewModelMappingProfile()
    {
        CreateMap<Cliente, ClienteViewModel>();

        CreateMap<NovoCliente, Cliente>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(x => StatusCliente.Ativo))
            .ForMember(d => d.Id, opt => opt.MapFrom(x => 0))
            .ForMember(d => d.Contas, opt => opt.MapFrom(x => new HashSet<Conta>()));

        CreateMap<AlteraCliente, Cliente>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(x => StatusCliente.Ativo))
            .ForMember(d => d.Contas, opt => opt.MapFrom(x => new HashSet<Conta>()));

        CreateMap<ReferenciaCliente, Cliente>();

        CreateMap<NovoTelefone, Telefone>()
            .ForMember(d => d.Id, opt => opt.MapFrom(x => 0))
            .ForMember(d => d.Cliente, opt => opt.MapFrom(x => new Cliente()));

        CreateMap<AlteraTelefone, Telefone>()
            .ForMember(d => d.Cliente, opt => opt.MapFrom(x => new Cliente()));

        CreateMap<Telefone, TelefoneViewModel>();

        CreateMap<NovoEndereco, Endereco>()
            .ForMember(d => d.Id, opt => opt.MapFrom(x => 0));

        CreateMap<Endereco, EnderecoViewModel>();
    }
}