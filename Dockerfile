FROM mcr.microsoft.com/dotnet/core/runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM amd64/buildpack-deps:bionic-scm AS build
WORKDIR /src
COPY ["ModuloFacturacion/ModuloFacturacion.csproj", "ModuloFacturacion/"]
COPY . .
WORKDIR "/src/ModuloFacturacion"
RUN sudo dotnet build "ModuloFacturacion.csproj" -c Release -o /app/build

FROM build AS publish
RUN sudo dotnet publish "ModuloFacturacion.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModuloFacturacion.dll"]
