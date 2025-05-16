using Projeli.StorageService.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStorageServiceSwagger();
builder.Services.AddStorageServiceServices();
builder.Services.AddStorageServiceRepositories();
builder.Services.AddStorageServiceOptions(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddStorageServiceAuthentication(builder.Configuration, builder.Environment);
builder.Services.UseStorageServiceRabbitMq(builder.Configuration);
builder.Services.AddStorageServiceOpenTelemetry(builder.Logging, builder.Configuration);

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseStorageServiceSwagger();
}

app.UseStorageServiceAuthentication();
app.UseStorageServiceOpenTelemetry();

app.Run();