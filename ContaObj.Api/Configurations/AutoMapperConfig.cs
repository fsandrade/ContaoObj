using ContaObj.Application.Mappings;

namespace ContaObj.Api.Configurations;

public static class AutoMapperConfig
{
    public static void AdicionaAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(
        typeof(AgenciaViewModelMappingProfile),
        typeof(ClienteViewModelMappingProfile),
        typeof(ContaViewModelMappingProfile),
        typeof(TransacaoMappingProfile));
    }
}