
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
COPY ["Services/ForexService/ForexService.Api/ForexService.Api.csproj", "Services/ForexService/ForexService.Api/"]
COPY ["BuildingBlocks/Framework.Core/Framework.Core.csproj", "BuildingBlocks/Framework.Core/"]
COPY ["BuildingBlocks/Framework.WebApi.Core/Framework.WebApi.Core.csproj", "BuildingBlocks/Framework.WebApi.Core/"]
COPY ["Services/ForexService/ForexService.Application/ForexService.Application.csproj", "Services/ForexService/ForexService.Application/"]
COPY ["Framework.Shared.IntegrationEvent/Framework.Shared.IntegrationEvent.csproj", "Framework.Shared.IntegrationEvent/"]
COPY ["BuildingBlocks/Framework.Message.Bus/Framework.Message.Bus.csproj", "BuildingBlocks/Framework.Message.Bus/"]
COPY ["Services/ForexService/ForexService.Domain/ForexService.Domain.csproj", "Services/ForexService/ForexService.Domain/"]
COPY ["Services/ForexService/ForexService.Infra/ForexService.Infra.csproj", "Services/ForexService/ForexService.Infra/"]
RUN dotnet restore "Services/ForexService/ForexService.Api/ForexService.Api.csproj"
COPY . .
WORKDIR "/src/Services/ForexService/ForexService.Api"
RUN dotnet build "ForexService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "ForexService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ForexService.Api.dll"]
