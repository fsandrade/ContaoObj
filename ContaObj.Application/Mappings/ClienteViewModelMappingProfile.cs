using AutoMapper;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel;

namespace ContaObj.Application.Mappings;

public class ClienteViewModelMappingProfile : Profile
{
    public ClienteViewModelMappingProfile()
    {
        CreateMap<ClienteViewModel, Cliente>().ReverseMap();
        CreateMap<NovoCliente, Cliente>().ReverseMap();
        CreateMap<AlteraCliente, Cliente>().ReverseMap();
    }
}

