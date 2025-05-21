using MassTransit;
using Projeli.Shared.Application.Messages.Files;
using Projeli.StorageService.Application.Services.Interfaces;
using File = Projeli.StorageService.Domain.Models.File;

namespace Projeli.StorageService.Api.Consumers;

public class FileStoreConsumer(IFileService fileService, IBus bus) : IConsumer<FileStoreMessage>
{
    public async Task Consume(ConsumeContext<FileStoreMessage> context)
    {
        var message = context.Message;
        var file = new File
        {
            Name = message.FileName,
            Extension = message.FileExtension,
            Type = message.FileType,
            ParentId = message.ParentId,
            Data = message.FileData,
            MimeType = message.MimeType
        };
        
        var result = await fileService.StoreFile(file);

        if (result is { Success: true, Data: not null })
        {
            await bus.Publish(new FileStoredMessage
            {
                FilePath = result.Data,
                FileType = message.FileType,
                ParentId = message.ParentId,
                UserId = message.UserId
            });
        }
        else
        {
            await bus.Publish(new FileStoreFailedMessage
            {
                FileName = message.FileName,
                FileExtension = message.FileExtension,
                MimeType = message.MimeType,
                FileType = message.FileType,
                ParentId = message.ParentId,
                FileData = message.FileData,
                UserId = message.UserId,
                ErrorMessages = result.Errors.Count != 0
                    ? result.Errors.SelectMany(e => e.Value).ToArray() 
                    : [result.Message ?? "Unknown error"]
            });
            
        }
    }
}