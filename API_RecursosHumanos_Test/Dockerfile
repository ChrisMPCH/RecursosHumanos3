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

# Publicar la aplicación
RUN dotnet publish "API_RecursosHumanos_Test/API_RecursosHumanos_Test.csproj" -c Release -o /app/publish

# Imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "API_RecursosHumanos_Test.dll"] 