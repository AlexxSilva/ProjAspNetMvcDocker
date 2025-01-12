# Usar a imagem oficial do .NET 8.0 para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Usar a imagem oficial do .NET 8.0 SDK para compilação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ProjAspNetMvcDocker.csproj", "./"]
RUN dotnet restore "ProjAspNetMvcDocker.csproj"
COPY . .
RUN dotnet build "ProjAspNetMvcDocker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjAspNetMvcDocker.csproj" -c Release -o /app/publish

# Etapa final: copiar a aplicação publicada e configurar o contêiner de execução
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjAspNetMvcDocker.dll"]