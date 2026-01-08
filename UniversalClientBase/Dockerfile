# ETAPA 1: Compilación (Build) 
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build 
WORKDIR /src 

# Copiar archivos de proyecto y restaurar dependencias 
COPY ["src/API/UniversalClientBase.API.csproj", "src/API/"] 
COPY ["src/Core/UniversalClientBase.Core.csproj", "src/Core/"] 
COPY ["src/Application/UniversalClientBase.Application.csproj", "src/Application/"] 
COPY ["src/Infrastructure/UniversalClientBase.Infrastructure.csproj", "src/Infrastructure/"] 
RUN dotnet restore "src/API/UniversalClientBase.API.csproj" 

# Copiar el resto del código y compilar 
COPY . . 
WORKDIR "/src/src/API" 
RUN dotnet build "UniversalClientBase.API.csproj" -c Release -o /app/build 

# ETAPA 2: Publicación 
FROM build AS publish 
RUN dotnet publish "UniversalClientBase.API.csproj" -c Release -o /app/publish /p:UseAppHost=false 
# ETAPA 3: Ejecución (Runtime) 
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final 
WORKDIR /app 
COPY --from=publish /app/publish . 

# Crear directorio para la base de datos persistente 
RUN mkdir -p /app/data && chown -R app:app /app/data

ENTRYPOINT ["dotnet", "UniversalClientBase.API.dll"]