using Projeli.Shared.Domain.Results;
using Projeli.StorageService.Application.Services.Interfaces;
using Projeli.StorageService.Domain.Repositories;
using File = Projeli.StorageService.Domain.Models.File;

namespace Projeli.StorageService.Application.Services;

public class FileService(IFileRepository repository) : IFileService
{
    public async Task<IResult<string>> StoreFile(File file)
    {
        var allowedExtensions = file.Type.MimeTypes;
        if (!allowedExtensions.Contains(file.MimeType))
        {
            return Result<string>.Fail($"The file type '{file.MimeType}' is not allowed. Allowed types are: {string.Join(", ", allowedExtensions)}");
        }
        
        if (file.Data.Length > file.Type.MaxSize)
        {
            return Result<string>.Fail($"The file size '{file.Data.Length}' exceeds the maximum allowed size of {file.Type.MaxSize} bytes.");
        }
        
        if (file.Type.RequiresParentId && string.IsNullOrEmpty(file.ParentId))
        {
            return Result<string>.Fail("The file requires a parent ID.");
        }
        
        string? path;

        try
        {
            path = await repository.StoreFile(file);
        } catch (Exception ex)
        {
            return Result<string>.Fail($"An error occurred while storing the file: {ex.Message}");
        }
        
        return string.IsNullOrEmpty(path) 
            ? Result<string>.Fail("Failed to store the file.") 
            : new Result<string>(path);
    }

    public async Task<IResult<bool>> DeleteFile(string filePath)
    {
        bool success;
        
        try
        {
            success = await repository.DeleteFile(filePath);
        }
        catch (Exception ex)
        {
            return Result<bool>.Fail($"An error occurred while deleting the file: {ex.Message}");
        }
        
        return success 
            ? new Result<bool>(success)
            : Result<bool>.Fail("Failed to delete the file.");
    }
}