FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FilmChooser.FilmCrawler/FilmChooser.FilmCrawler.csproj", "FilmChooser.FilmCrawler/"]
RUN dotnet restore "FilmChooser.FilmCrawler/FilmChooser.FilmCrawler.csproj"
COPY . .
WORKDIR "/src/FilmChooser.FilmCrawler"
RUN dotnet build "FilmChooser.FilmCrawler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FilmChooser.FilmCrawler.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FilmChooser.FilmCrawler.dll"]
