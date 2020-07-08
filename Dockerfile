FROM mcr.microsoft.com/dotnet/core/runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["UnitTestProject/UnitTestProject.csproj", "UnitTestProject/"]
COPY ["ModuloFacturacion/ModuloFacturacion.csproj", "ModuloFacturacion/"]
WORKDIR "/src/UnitTestProject"
RUN dotnet restore UnitTestProject.csproj -r linux-musl-x64
COPY . .
WORKDIR "/src/ModuloFacturacion"
RUN dotnet restore ModuloFacturacion.csproj -r linux-musl-x64
COPY . .
WORKDIR "/src/UnitTestProject"
RUN dotnet build UnitTestProject.csproj -c Release -o /app/build -r linux-musl-x64
WORKDIR "/src/ModuloFacturacion"
RUN dotnet build ModuloFacturacion.csproj -c Release -o /app/build -r linux-musl-x64

FROM build AS publish
RUN dotnet publish ModuloFacturacion.csproj -c Release -o /app/publish -r linux-musl-x64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModuloFacturacion.dll"]
