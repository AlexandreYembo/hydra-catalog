#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./dotnet3.1/Hydra.Catalog.API/Hydra.Catalog.API.csproj", "dotnet3.1/Hydra.Catalog.API/"]
COPY ["./hydra-core/dotnet3.1/Hydra.Core/Hydra.Core.csproj", "building blocks/Hydra.Core/"]
COPY ["./hydra-core/dotnet3.1/Hydra.WebAPI.Core/Hydra.WebAPI.Core.csproj", "building blocks/Hydra.WebAPI.Core/"]
COPY ["./hydra-core/dotnet3.1/Hydra.Core.MessageBus/Hydra.Core.MessageBus.csproj", "building blocks/Hydra.Core.MessageBus/"]
COPY ["./hydra-core/dotnet3.1/Hydra.Core.Integration/Hydra.Core.Integration.csproj", "building blocks/Hydra.Core.Integration/"]

RUN dotnet restore "dotnet3.1/Hydra.Catalog.API/Hydra.Catalog.API.csproj"
COPY . .
WORKDIR "dotnet3.1/Hydra.Catalog.API"
RUN dotnet build "Hydra.Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hydra.Catalog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hydra.Catalog.API.dll"]