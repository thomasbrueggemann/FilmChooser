﻿FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FilmChooser.CameraCrawler.csproj", "./"]
RUN dotnet restore "FilmChooser.CameraCrawler.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "FilmChooser.CameraCrawler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FilmChooser.CameraCrawler.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FilmChooser.CameraCrawler.dll"]
