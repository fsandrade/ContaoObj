using AutoMapper;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel.Agencia;

namespace ContaObj.Application.Mappings
{
    public class AgenciaViewModelMappingProfile : Profile
    {
        public AgenciaViewModelMappingProfile()
        {
            CreateMap<AgenciaViewModel, Agencia>().ReverseMap();
        }
    }
}