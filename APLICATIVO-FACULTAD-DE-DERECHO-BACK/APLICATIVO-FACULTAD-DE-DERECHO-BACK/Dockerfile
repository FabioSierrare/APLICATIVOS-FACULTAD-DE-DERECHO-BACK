# Imagen base para compilar el proyecto
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Imagen base para ejecutar el proyecto
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "APLICATIVO-FACULTAD-DE-DERECHO-BACK.dll"]
