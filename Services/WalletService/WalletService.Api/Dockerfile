
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
COPY ["Services/WalletService/WalletService.Api/WalletService.Api.csproj", "Services/WalletService/WalletService.Api/"]
COPY ["BuildingBlocks/Framework.Core/Framework.Core.csproj", "BuildingBlocks/Framework.Core/"]
COPY ["BuildingBlocks/Framework.WebApi.Core/Framework.WebApi.Core.csproj", "BuildingBlocks/Framework.WebApi.Core/"]
COPY ["Services/WalletService/WalletService.Application/WalletService.Application.csproj", "Services/WalletService/WalletService.Application/"]
COPY ["Framework.Shared.IntegrationEvent/Framework.Shared.IntegrationEvent.csproj", "Framework.Shared.IntegrationEvent/"]
COPY ["BuildingBlocks/Framework.Message.Bus/Framework.Message.Bus.csproj", "BuildingBlocks/Framework.Message.Bus/"]
COPY ["Services/WalletService/WalletService.Domain/WalletService.Domain.csproj", "Services/WalletService/WalletService.Domain/"]
COPY ["Services/WalletService/WalletService.Infra/WalletService.Infra.csproj", "Services/WalletService/WalletService.Infra/"]
RUN dotnet restore "Services/WalletService/WalletService.Api/WalletService.Api.csproj"
COPY . .
WORKDIR "/src/Services/WalletService/WalletService.Api"
RUN dotnet build "WalletService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "WalletService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WalletService.Api.dll"]
