# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar los archivos de proyecto
COPY ["API_RecursosHumanos_Test/API_RecursosHumanos_Test.csproj", "API_RecursosHumanos_Test/"]
COPY ["RecursosHumanosCore/RecursosHumanosCore.csproj", "RecursosHumanosCore/"]

# Restaurar dependencias
RUN dotnet restore "API_RecursosHumanos_Test/API_RecursosHumanos_Test.csproj"

# Copiar el resto del código
COPY . .

# Usar WORKDIR para build y publish para que los paths sean relativos
WORKDIR /src/API_RecursosHumanos_Test

# Compilar la aplicación
RUN dotnet build "API_RecursosHumanos_Test.csproj" -c Release -o /app/build

# Publicar la aplicación
RUN dotnet publish "API_RecursosHumanos_Test.csproj" -c Release -o /app/publish

# Imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Instalar curl para health check
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Copiar los archivos publicados
COPY --from=build /app/publish .

# Variables de entorno
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80
ENV ConnectionStrings__DefaultConnection="Host=dpg-d0knhi7fte5s738rd8hg-a.oregon-postgres.render.com;Port=5432;Database=recursoshumanos;Username=recursoshumanos_user;Password=UpgPeaoHYijsjjagibEF6m8dzcXyNP3j;SSL Mode=Require;Trust Server Certificate=true;"

# Exponer puertos
EXPOSE 80
EXPOSE 443

# Health check
HEALTHCHECK --interval=30s --timeout=3s --retries=3 \
    CMD curl -f http://localhost:80/health || exit 1

# Ejecutar la app
ENTRYPOINT ["dotnet", "API_RecursosHumanos_Test.dll"]
