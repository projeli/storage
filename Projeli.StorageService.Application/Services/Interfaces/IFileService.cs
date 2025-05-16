using Projeli.Shared.Domain.Results;
using File = Projeli.StorageService.Domain.Models.File;

namespace Projeli.StorageService.Application.Services.Interfaces;

public interface IFileService
{
    Task<IResult<string>> StoreFile(File file);
    Task<IResult<bool>> DeleteFile(string filePath);
}