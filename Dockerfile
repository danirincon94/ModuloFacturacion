FROM mcr.microsoft.com/dotnet/core/runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["ModuloFacturacion/ModuloFacturacion.csproj", "ModuloFacturacion/"]
WORKDIR "/src/ModuloFacturacion"
RUN dotnet restore ModuloFacturacion.csproj -r linux-musl-x64
COPY . .
WORKDIR "/src/UnitTestProject"
RUN dotnet test UnitTestProject.csproj

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModuloFacturacion.dll"]
