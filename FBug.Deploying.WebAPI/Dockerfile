FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["FBug.Deploying.WebAPI/FBug.Deploying.WebAPI.csproj", "FBug.Deploying.WebAPI/"]
COPY . .
WORKDIR "/src/FBug.Deploying.WebAPI"
RUN dotnet build "FBug.Deploying.WebAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FBug.Deploying.WebAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FBug.Deploying.WebAPI.dll"]