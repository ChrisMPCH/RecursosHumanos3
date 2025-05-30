# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar los archivos de proyecto y restaurar dependencias
COPY ["API_RecursosHumanos_Test/API_RecursosHumanos_Test.csproj", "API_RecursosHumanos_Test/"]
COPY ["RecursosHumanosCore/RecursosHumanosCore.csproj", "RecursosHumanosCore/"]
RUN dotnet restore "API_RecursosHumanos_Test/API_RecursosHumanos_Test.csproj"

# Copiar el resto del código
COPY . .

# Compilar la aplicación
WORKDIR "/src/API_RecursosHumanos_Test"
RUN dotnet build "API_RecursosHumanos_Test.csproj" -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "API_RecursosHumanos_Test.csproj" -c Release -o /app/publish

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Exponer el puerto que usará la aplicación
EXPOSE 80
EXPOSE 443

# Establecer la variable de entorno para el puerto
ENV ASPNETCORE_URLS=http://+:80

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "API_RecursosHumanos_Test.dll"] 