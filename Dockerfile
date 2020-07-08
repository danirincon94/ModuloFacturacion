FROM mcr.microsoft.com/dotnet/core/runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM amd64/buildpack-deps:bionic-scm AS build
WORKDIR /src
COPY ["ModuloFacturacion/ModuloFacturacion.csproj", "ModuloFacturacion/"]
RUN dotnet restore "ModuloFacturacion/ModuloFacturacion.csproj"
COPY . .
WORKDIR "/src/ModuloFacturacion"
RUN dotnet build "ModuloFacturacion.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ModuloFacturacion.csproj" -c Release -o /app/publish -r linux-musl-x64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModuloFacturacion.dll"]
