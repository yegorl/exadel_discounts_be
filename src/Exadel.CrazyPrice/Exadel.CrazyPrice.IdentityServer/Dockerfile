#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 8001

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Exadel.CrazyPrice.IdentityServer/Exadel.CrazyPrice.IdentityServer.csproj", "Exadel.CrazyPrice.IdentityServer/"]
COPY ["Exadel.CrazyPrice.Data/Exadel.CrazyPrice.Data.csproj", "Exadel.CrazyPrice.Data/"]
COPY ["Exadel.CrazyPrice.Common/Exadel.CrazyPrice.Common.csproj", "Exadel.CrazyPrice.Common/"]
RUN dotnet restore "Exadel.CrazyPrice.IdentityServer/Exadel.CrazyPrice.IdentityServer.csproj"
COPY . .
WORKDIR "/src/Exadel.CrazyPrice.IdentityServer"
RUN dotnet build "Exadel.CrazyPrice.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Exadel.CrazyPrice.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Exadel.CrazyPrice.IdentityServer.dll"]