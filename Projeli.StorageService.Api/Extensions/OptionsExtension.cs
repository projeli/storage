using Projeli.StorageService.Application.Options;

namespace Projeli.StorageService.Api.Extensions;

public static class OptionsExtension
{
    public static void AddStorageServiceOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AwsOptions>(configuration.GetSection(AwsOptions.Section));
    }
}