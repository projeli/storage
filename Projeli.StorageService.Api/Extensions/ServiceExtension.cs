using Projeli.StorageService.Application.Services;
using Projeli.StorageService.Application.Services.Interfaces;

namespace Projeli.StorageService.Api.Extensions;

public static class ServiceExtension
{
    public static void AddStorageServiceServices(this IServiceCollection services)
    {
        services.AddScoped<IFileService, FileService>();
    }
}