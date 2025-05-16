using System.Text.Json;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using Projeli.StorageService.Application.Options;
using Projeli.StorageService.Domain.Repositories;
using File = Projeli.StorageService.Domain.Models.File;

namespace Projeli.StorageService.Infrastructure.Repositories;

public class FileRepository(IOptions<AwsOptions> options) : IFileRepository
{
    private readonly AwsOptions _options = options.Value;

    private AmazonS3Client GetS3Client()
    {
        var credentials = new BasicAWSCredentials(_options.AccessKey, _options.SecretKey);
        return new AmazonS3Client(credentials, new AmazonS3Config
        {
            ServiceURL = _options.ServiceUrl
        });
    }

    public async Task<string?> StoreFile(File file)
    {
        var fileName = string.IsNullOrEmpty(file.ParentId)
            ? $"{file.Type.Subdirectory}/{file.Name}.{file.Extension}"
            : $"{file.Type.Subdirectory}/{file.ParentId}/{file.Name}.{file.Extension}";

        Console.WriteLine($"Filename: {fileName}");
        
        var request = new PutObjectRequest
        {
            BucketName = _options.BucketName,
            Key = fileName,
            InputStream = new MemoryStream(file.Data),
            ContentType = file.MimeType,
            DisablePayloadSigning = true
        };

        await GetS3Client().PutObjectAsync(request);
        return fileName;
    }

    public async Task<bool> DeleteFile(string filePath)
    {
        var request = new DeleteObjectRequest
        {
            BucketName = _options.BucketName,
            Key = filePath
        };
        
        await GetS3Client().DeleteObjectAsync(request);
        return true;
    }
}