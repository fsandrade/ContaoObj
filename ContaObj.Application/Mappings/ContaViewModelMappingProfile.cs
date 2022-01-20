using AutoMapper;
using ContaObj.Domain.Enumerations;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel;
using ContaObj.Domain.ViewModel.Conta;
using ContaObj.Domain.ViewModel.Transacao;

namespace ContaObj.Application.Mappings
{
    public class ContaViewModelMappingProfile : Profile
    {
        public ContaViewModelMappingProfile()
        {
            CreateMap<ContaViewModel, Conta>().ReverseMap();
            CreateMap<NovaConta, Conta>();
            CreateMap<Transacao, NovaTransacao>().ReverseMap();

            CreateMap<NovaConta, Conta>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(x => StatusConta.Ativa))
                .ForMember(dest => dest.Agencia, opt => opt.MapFrom(x => new Agencia { Id = x.Agencia }))
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom(x => new Cliente { Id = x.Cliente }));
        }
    }
}