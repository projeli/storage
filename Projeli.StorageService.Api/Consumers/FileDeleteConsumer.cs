using MassTransit;
using Projeli.Shared.Infrastructure.Messaging.Events;
using Projeli.StorageService.Application.Services.Interfaces;

namespace Projeli.StorageService.Api.Consumers;

public class FileDeleteConsumer(IFileService fileService) : IConsumer<FileDeleteEvent>
{
    public async Task Consume(ConsumeContext<FileDeleteEvent> context)
    {
        var message = context.Message;
        await fileService.DeleteFile(message.FilePath);
    }
}