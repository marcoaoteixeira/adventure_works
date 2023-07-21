#See https://aka.ms/customizecontainer to learn how to customize your debug container
#and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Nameless.AdventureWorks.IdentityServer/nuget.config", "Nameless.AdventureWorks.IdentityServer/"]
COPY ["Nameless.AdventureWorks.IdentityServer/Nameless.AdventureWorks.IdentityServer.csproj", "Nameless.AdventureWorks.IdentityServer/"]
RUN dotnet restore "Nameless.AdventureWorks.IdentityServer/Nameless.AdventureWorks.IdentityServer.csproj"
COPY . .
WORKDIR "/src/Nameless.AdventureWorks.IdentityServer"
RUN dotnet build "Nameless.AdventureWorks.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nameless.AdventureWorks.IdentityServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nameless.AdventureWorks.IdentityServer.dll"]