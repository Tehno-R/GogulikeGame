FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Roguelike/Roguelike.csproj", "Roguelike/"]
RUN dotnet restore "Roguelike/Roguelike.csproj"
COPY . .
WORKDIR "/src/Roguelike"
RUN dotnet build "Roguelike.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Roguelike.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Roguelike.dll"]
