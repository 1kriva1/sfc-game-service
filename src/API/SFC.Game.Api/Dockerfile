#base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
LABEL service="SFC.Game.Api" author="Kryvoruk Andrii" email="krivorukandrey@gmail.com"
USER root
WORKDIR /app

#restore & build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ENV HUSKY=False
WORKDIR /source
COPY ["Directory.Build.props", "."]
COPY ["nuget.config", "."]
COPY ["src/API/SFC.Game.Api/SFC.Game.Api.csproj", "src/API/SFC.Game.Api/"]
COPY ["src/Core/SFC.Game.Application/SFC.Game.Application.csproj", "src/Core/SFC.Game.Application/"]
COPY ["src/Core/SFC.Game.Domain/SFC.Game.Domain.csproj", "src/Core/SFC.Game.Domain/"]
COPY ["src/Infrastructure/SFC.Game.Infrastructure/SFC.Game.Infrastructure.csproj", "src/Infrastructure/SFC.Game.Infrastructure/"]
COPY ["src/Infrastructure/SFC.Game.Infrastructure.Persistence/SFC.Game.Infrastructure.Persistence.csproj", "src/Infrastructure/SFC.Game.Infrastructure.Persistence/"]
COPY ["src/Contracts/SFC.Game.Contracts/SFC.Game.Contracts.csproj", "src/Contracts/SFC.Game.Contracts/"]
COPY ["src/Contracts/SFC.Game.Messages/SFC.Game.Messages.csproj", "src/Contracts/SFC.Game.Messages/"]
RUN dotnet restore "./src/API/SFC.Game.Api/SFC.Game.Api.csproj" --property:HUSKY=$HUSKY
COPY [".", "."]
WORKDIR "/source/src/API/SFC.Game.Api"
RUN dotnet build "./SFC.Game.Api.csproj" --configuration $BUILD_CONFIGURATION --property:HUSKY=$HUSKY --output /app/build

#publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SFC.Game.Api.csproj" --configuration $BUILD_CONFIGURATION --property:HUSKY=$HUSKY --property:UseAppHost=false --output /app/publish

#final
FROM base AS final
ARG HTTP_PORT=8080
ARG HTTPS_PORT=8081
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE $HTTP_PORT
EXPOSE $HTTPS_PORT
ENTRYPOINT ["bash", "entrypoint.sh"]