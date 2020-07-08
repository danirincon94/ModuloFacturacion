FROM mcr.microsoft.com/dotnet/core/runtime AS base
WORKDIR /app
COPY . .

RUN dotnet restore

# run tests on docker build
RUN dotnet test
ENTRYPOINT ["dotnet", "ModuloFacturacion.dll"]
