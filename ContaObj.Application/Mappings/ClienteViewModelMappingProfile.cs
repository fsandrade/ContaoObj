using AutoMapper;
using ContaObj.Domain.Enumerations;
using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel;

namespace ContaObj.Application.Mappings;

public class ClienteViewModelMappingProfile : Profile
{
    public ClienteViewModelMappingProfile()
    {
        CreateMap<ClienteViewModel, Cliente>().ReverseMap();

        CreateMap<NovoCliente, Cliente>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(x => StatusCliente.Ativo));

        CreateMap<AlteraCliente, Cliente>();
        CreateMap<AlteraTelefone, Telefone>();
        CreateMap<NovoEndereco, Endereco>().ReverseMap();
        CreateMap<NovoTelefone, Telefone>().ReverseMap();
    }
}

