using File = Projeli.StorageService.Domain.Models.File;

namespace Projeli.StorageService.Domain.Repositories;

public interface IFileRepository
{
    Task<string?> StoreFile(File file);
    Task<bool> DeleteFile(string filePath);
}