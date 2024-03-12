#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StandUsersBridge/StandUsersBridge.csproj", "StandUsersBridge/"]
COPY ["StandUsersBridge.Domain/StandUsersBridge.Domain.csproj", "StandUsersBridge.Domain/"]
COPY ["StandUsersBridge.Infrastructure/StandUsersBridge.Infrastructure.csproj", "StandUsersBridge.Infrastructure/"]
RUN dotnet restore "./StandUsersBridge/./StandUsersBridge.csproj"
COPY . .
WORKDIR "/src/StandUsersBridge"
RUN dotnet build "./StandUsersBridge.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StandUsersBridge.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StandUsersBridge.dll"]