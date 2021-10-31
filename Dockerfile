FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FilmChooser.csproj", "./"]
RUN dotnet restore "FilmChooser.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "FilmChooser.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FilmChooser.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FilmChooser.dll"]
