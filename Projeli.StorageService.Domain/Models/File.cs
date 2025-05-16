using Projeli.Shared.Domain.Models.Files;

namespace Projeli.StorageService.Domain.Models;

public class File
{
    public string Name { get; set; } = null!;
    public string Extension { get; set; } = null!;
    public string MimeType { get; set; } = null!;
    public FileType Type { get; set; } = null!;
    public string? ParentId { get; set; }
    public byte[] Data { get; set; } = null!;
}