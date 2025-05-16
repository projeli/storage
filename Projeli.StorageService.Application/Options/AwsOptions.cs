namespace Projeli.StorageService.Application.Options;

public class AwsOptions
{
    public const string Section = "AWS";
    
    public string AccessKey { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public string ServiceUrl { get; set; } = null!;
    public string BucketName { get; set; } = null!;
}