FROM mcr.microsoft.com/dotnet/core/runtime AS base
WORKDIR /app
COPY . .

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
RUN dotnet restore ModuloFacturacion.csproj

# run tests on docker build
RUN dotnet test UnitTestProject.csproj
ENTRYPOINT ["dotnet", "ModuloFacturacion.dll"]
