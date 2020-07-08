FROM mcr.microsoft.com/dotnet/core/runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["UnitTestProject/UnitTestProject.csproj", "UnitTestProject/"]
COPY ["ModuloFacturacion/ModuloFacturacion.csproj", "ModuloFacturacion/"]
WORKDIR "/src/UnitTestProject"
RUN dotnet restore UnitTestProject.csproj
COPY . .
WORKDIR "/src/ModuloFacturacion"
RUN dotnet restore ModuloFacturacion.csproj
COPY . .

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModuloFacturacion.dll"]
