#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WillyNet.Charlas.Presentation.WebApi/WillyNet.Charlas.Presentation.WebApi.csproj", "WillyNet.Charlas.Presentation.WebApi/"]
RUN dotnet restore "WillyNet.Charlas.Presentation.WebApi/WillyNet.Charlas.Presentation.WebApi.csproj"
COPY . .
WORKDIR "/src/WillyNet.Charlas.Presentation.WebApi"
RUN dotnet build "WillyNet.Charlas.Presentation.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WillyNet.Charlas.Presentation.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WillyNet.Charlas.Presentation.WebApi.dll"]