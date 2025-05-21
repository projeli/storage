using MassTransit;
using Projeli.Shared.Application.Messages.Files;
using Projeli.StorageService.Application.Services.Interfaces;

namespace Projeli.StorageService.Api.Consumers;

public class FileDeleteConsumer(IFileService fileService) : IConsumer<FileDeleteMessage>
{
    public async Task Consume(ConsumeContext<FileDeleteMessage> context)
    {
        var message = context.Message;
        await fileService.DeleteFile(message.FilePath);
    }
}