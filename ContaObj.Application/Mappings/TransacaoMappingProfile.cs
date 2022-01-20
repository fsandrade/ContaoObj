using AutoMapper;
using ContaObj.Domain.Enumerations;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel.Transacao;

namespace ContaObj.Application.Mappings;

public class TransacaoMappingProfile : Profile
{
    public TransacaoMappingProfile()
    {
        CreateMap<NovaTransacao, Transacao>()
            .ForMember(p => p.Origem, o => o.MapFrom(x => new Conta { Id = x.Origem }))
            .ForMember(p => p.Destino, o => o.MapFrom(x => new Conta { Id = x.Destino }))
            .ForMember(p => p.Data, o => o.MapFrom(_ => DateTime.Now))
            .ForMember(p => p.Status, o => o.MapFrom(_ => StatusTransacao.Iniciada));

        CreateMap<Transacao, TransacaoViewModel>()
            .ForMember(p => p.Origem, o => o.MapFrom(x => x.Origem.Id))
            .ForMember(p => p.Destino, o => o.MapFrom(x => x.Destino.Id));
    }
}