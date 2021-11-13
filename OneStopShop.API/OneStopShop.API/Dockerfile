FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 11201

ENV ASPNETCORE_URLS=http://+:11201

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["OneStopShop.API.csproj", "./"]
RUN dotnet restore "OneStopShop.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "OneStopShop.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OneStopShop.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OneStopShop.API.dll"]
