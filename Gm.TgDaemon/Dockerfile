FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Gm.Daemon/Gm.Daemon.csproj", "Gm.Daemon/"]
RUN dotnet restore "Gm.Daemon/Gm.Daemon.csproj"
COPY . .
WORKDIR "/src/Gm.Daemon"
RUN dotnet build "Gm.Daemon.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gm.Daemon.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gm.Daemon.dll"]
