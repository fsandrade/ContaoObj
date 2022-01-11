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

        CreateMap<NovoTelefone, Telefone>();
        CreateMap<AlteraTelefone, Telefone>();
        CreateMap<Telefone, TelefoneViewModel>().ReverseMap();

        CreateMap<NovoEndereco, Endereco>().ReverseMap();
        CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
    }
}

