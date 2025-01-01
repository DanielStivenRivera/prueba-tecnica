FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# Agregar variable de entorno para el UID del usuario de la aplicación
ENV APP_UID=1000
USER $APP_UID
WORKDIR /app
# Variables de entorno para puertos expuestos
ENV APP_PORT=8080
ENV DEBUG_PORT=8081
EXPOSE $APP_PORT
EXPOSE $DEBUG_PORT

# Variable de entorno para la cadena de conexión
ENV ConnectionStrings__DefaultConnection="Host=localhost;Database=postgres;Username=user;Password=pass123"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Agregar variable de entorno para la configuración de compilación
ARG BUILD_CONFIGURATION=Release
ENV BUILD_CONFIGURATION=$BUILD_CONFIGURATION
WORKDIR /src
COPY ["app-server.csproj", "./"]
RUN dotnet restore "app-server.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "app-server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
# Reutilizar la configuración de compilación en publish
ARG BUILD_CONFIGURATION=Release
ENV BUILD_CONFIGURATION=$BUILD_CONFIGURATION
RUN dotnet publish "app-server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
# Variables de entorno para configuración final
ENV DOTNET_ENVIRONMENT=Production
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "app-server.dll"]
