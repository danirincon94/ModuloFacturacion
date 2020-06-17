FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1809 AS build
WORKDIR /src
COPY ["ModuloFacturacion/ModuloFacturacion.csproj", "ModuloFacturacion/"]
COPY . .
WORKDIR "/src/ModuloFacturacion"
RUN dotnet build "ModuloFacturacion.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ModuloFacturacion.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModuloFacturacion.dll"]
