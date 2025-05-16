FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["storage/Projeli.StorageService.Api/Projeli.StorageService.Api.csproj", "storage/Projeli.StorageService.Api/"]
COPY ["shared/Projeli.Shared.Domain/Projeli.Shared.Domain.csproj", "shared/Projeli.Shared.Domain/"]
COPY ["shared/Projeli.Shared.Application/Projeli.Shared.Application.csproj", "shared/Projeli.Shared.Application/"]
COPY ["shared/Projeli.Shared.Infrastructure/Projeli.Shared.Infrastructure.csproj", "shared/Projeli.Shared.Infrastructure/"]
RUN dotnet restore "storage/Projeli.StorageService.Api/Projeli.StorageService.Api.csproj"
COPY ["storage", "storage"]
COPY ["shared", "shared"]
WORKDIR "/src/storage/Projeli.StorageService.Api"
RUN dotnet build "Projeli.StorageService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Projeli.StorageService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Projeli.StorageService.Api.dll"]
