#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster AS build
WORKDIR /src
COPY ["Exadel.CrazyPrice.IdentityServer/Exadel.CrazyPrice.IdentityServer.csproj", "Exadel.CrazyPrice.IdentityServer/"]
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