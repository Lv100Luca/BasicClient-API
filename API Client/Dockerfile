FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["API Client/API Client.csproj", "API Client/"]
RUN dotnet restore "API Client/API Client.csproj"
COPY . .
WORKDIR "/src/API Client"
RUN dotnet build "API Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API Client.dll"]
