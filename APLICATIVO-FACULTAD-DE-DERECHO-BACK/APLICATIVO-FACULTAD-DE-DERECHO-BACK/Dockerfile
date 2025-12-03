# Imagen base para compilar el proyecto
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Imagen base para ejecutar el proyecto
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# MUY IMPORTANTE: Render necesita esto
EXPOSE 8080

COPY --from=build /app .

# Igual de importante: decirle .NET que escuche en todas las interfaces
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

ENTRYPOINT ["dotnet", "APLICATIVO-FACULTAD-DE-DERECHO-BACK.dll"]
