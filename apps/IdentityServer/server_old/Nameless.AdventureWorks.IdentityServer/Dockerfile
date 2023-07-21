# See https://aka.ms/customizecontainer to learn how to customize your debug container
# and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["apps/identity_server/server/Nameless.AdventureWorks.IdentityServer/*.csproj", "apps/identity_server/server/Nameless.AdventureWorks.IdentityServer/"]
COPY ["libs/Nameless.AdventureWorks.Core/*.csproj", "libs/Nameless.AdventureWorks.Core/"]
RUN dotnet restore "apps/identity_server/server/Nameless.AdventureWorks.IdentityServer/Nameless.AdventureWorks.IdentityServer.csproj"
COPY . .
WORKDIR "/src/apps/identity_server/server/Nameless.AdventureWorks.IdentityServer"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nameless.AdventureWorks.IdentityServer.dll"]