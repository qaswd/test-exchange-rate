FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY . .

RUN ls -R /src
RUN find /src

RUN dotnet restore ExchangeRate/ExchangeRate.csproj
RUN dotnet publish ExchangeRate/ExchangeRate.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ExchangeRate.dll"]
