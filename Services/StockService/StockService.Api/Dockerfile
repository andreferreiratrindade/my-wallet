
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
COPY ["Services/StockService/StockService.Api/StockService.Api.csproj", "Services/StockService/StockService.Api/"]
COPY ["BuildingBlocks/Framework.Core/Framework.Core.csproj", "BuildingBlocks/Framework.Core/"]
COPY ["BuildingBlocks/Framework.WebApi.Core/Framework.WebApi.Core.csproj", "BuildingBlocks/Framework.WebApi.Core/"]
COPY ["Services/StockService/StockService.Application/StockService.Application.csproj", "Services/StockService/StockService.Application/"]
COPY ["Framework.Shared.IntegrationEvent/Framework.Shared.IntegrationEvent.csproj", "Framework.Shared.IntegrationEvent/"]
COPY ["BuildingBlocks/Framework.Message.Bus/Framework.Message.Bus.csproj", "BuildingBlocks/Framework.Message.Bus/"]
COPY ["Services/StockService/StockService.Domain/StockService.Domain.csproj", "Services/StockService/StockService.Domain/"]
COPY ["Services/StockService/StockService.Infra/StockService.Infra.csproj", "Services/StockService/StockService.Infra/"]
RUN dotnet restore "Services/StockService/StockService.Api/StockService.Api.csproj"
COPY . .
WORKDIR "/src/Services/StockService/StockService.Api"
RUN dotnet build "StockService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "StockService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockService.Api.dll"]
