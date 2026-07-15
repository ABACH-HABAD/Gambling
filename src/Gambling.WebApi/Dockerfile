FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 as build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["/src", "."]
RUN dotnet restore "./Gambling.WebApi/Gambling.WebApi.csproj"
COPY . .
RUN dotnet build "./Gambling.WebApi/Gambling.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Gambling.WebApi/Gambling.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENTRYPOINT ["dotnet", "Gambling.WebApi.dll"]