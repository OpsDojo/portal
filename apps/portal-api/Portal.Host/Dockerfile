FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Portal.Host/Portal.Host.csproj", "Portal.Host/"]
COPY ["Portal.Infrastructure/Portal.Infrastructure.csproj", "Portal.Infrastructure/"]
COPY ["Portal.Application/Portal.Application.csproj", "Portal.Application/"]
COPY ["Portal.Domain/Portal.Domain.csproj", "Portal.Domain/"]
RUN dotnet restore "./Portal.Host/Portal.Host.csproj"
COPY . .
WORKDIR "/src/Portal.Host"
RUN dotnet build "./Portal.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Portal.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Portal.Host.dll"]