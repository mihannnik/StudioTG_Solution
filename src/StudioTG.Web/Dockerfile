FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/StudioTG.Web/StudioTG.Web.csproj", "src/StudioTG.Web/"]
COPY ["src/StudioTG.Application/StudioTG.Application.csproj", "src/StudioTG.Application/"]
COPY ["src/StudioTG.Domain/StudioTG.Domain.csproj", "src/StudioTG.Domain/"]
COPY ["src/StudioTG.Infrastructure/StudioTG.Infrastructure.csproj", "src/StudioTG.Infrastructure/"]

RUN dotnet restore "./src/StudioTG.Web/StudioTG.Web.csproj"
COPY . . 
WORKDIR src/StudioTG.Web
RUN dotnet build "./StudioTG.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StudioTG.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudioTG.Web.dll"]