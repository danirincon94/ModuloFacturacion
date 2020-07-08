FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /source
COPY "ModuloFacturacion/ModuloFacturacion.csproj" .
RUN dotnet restore -r linux-musl-x64
COPY . .
RUN dotnet publish -c release -o /app -r linux-musl-x64 --self-contained true --no-restore /p:PublishTrimmed=true /p:PublishReadyToRun=true
FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.1-alpine
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ModuloFacturacion.dll"]
