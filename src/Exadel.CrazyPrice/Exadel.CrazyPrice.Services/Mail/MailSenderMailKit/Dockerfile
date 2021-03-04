#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 7000

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS publish
WORKDIR /src
COPY . .
RUN dotnet restore "Exadel.CrazyPrice.Services/Mail/MailSenderMailKit/MailSenderMailKit.csproj"
WORKDIR "/src/Exadel.CrazyPrice.Services/Mail/MailSenderMailKit"
RUN dotnet publish "MailSenderMailKit.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MailSenderMailKit.dll"]