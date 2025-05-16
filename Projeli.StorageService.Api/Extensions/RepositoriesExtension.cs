using Projeli.StorageService.Application.Services;
using Projeli.StorageService.Application.Services.Interfaces;
using Projeli.StorageService.Domain.Repositories;
using Projeli.StorageService.Infrastructure.Repositories;

namespace Projeli.StorageService.Api.Extensions;

public static class RepositoriesExtension
{
    public static void AddStorageServiceRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFileRepository, FileRepository>();
    }
}