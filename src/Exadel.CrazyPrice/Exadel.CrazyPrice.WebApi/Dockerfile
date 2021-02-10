#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 9001

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Exadel.CrazyPrice.WebApi/Exadel.CrazyPrice.WebApi.csproj", "Exadel.CrazyPrice.WebApi/"]
COPY ["Exadel.CrazyPrice.Data/Exadel.CrazyPrice.Data.csproj", "Exadel.CrazyPrice.Data/"]
COPY ["Exadel.CrazyPrice.Common/Exadel.CrazyPrice.Common.csproj", "Exadel.CrazyPrice.Common/"]
RUN dotnet restore "Exadel.CrazyPrice.WebApi/Exadel.CrazyPrice.WebApi.csproj"
COPY . .
WORKDIR "/src/Exadel.CrazyPrice.WebApi"
RUN dotnet build "Exadel.CrazyPrice.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Exadel.CrazyPrice.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Exadel.CrazyPrice.WebApi.dll"]