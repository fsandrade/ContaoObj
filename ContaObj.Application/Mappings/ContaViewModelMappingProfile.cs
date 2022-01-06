using AutoMapper;
using ContaObj.Domain.Enumerations;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel;

namespace ContaObj.Application.Mappings
{
    public class ContaViewModelMappingProfile : Profile
    {
        public ContaViewModelMappingProfile()
        {
            CreateMap<ContaViewModel, Conta>().ReverseMap();
            //CreateMap<AlteraConta, Conta>().ReverseMap();
            //CreateMap<NovaConta, Conta>().ReverseMap();
            CreateMap<Transacao, TransacaoViewModel> ().ReverseMap();

            CreateMap<NovaConta, Conta>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(x => StatusConta.Ativa))
                .ForMember(dest => dest.Limite, opt => opt.MapFrom(x => 500));
        }
    }
}
