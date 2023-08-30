#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Identity/GeekStore.Identity.WebAPI/GeekStore.Identity.WebAPI.csproj", "src/Services/Identity/GeekStore.Identity.WebAPI/"]
COPY ["src/BuildingBlocks/GeekStore.Core/GeekStore.Core.csproj", "src/BuildingBlocks/GeekStore.Core/"]
COPY ["src/BuildingBlocks/GeekStore.EventSourcing/GeekStore.EventSourcing.csproj", "src/BuildingBlocks/GeekStore.EventSourcing/"]
COPY ["src/BuildingBlocks/GeekStore.WebApi.Core/GeekStore.WebApi.Core.csproj", "src/BuildingBlocks/GeekStore.WebApi.Core/"]
COPY ["src/Services/Identity/GeekStore.Identity.Application/GeekStore.Identity.Application.csproj", "src/Services/Identity/GeekStore.Identity.Application/"]
COPY ["src/BuildingBlocks/GeekStore.MessageBus/GeekStore.MessageBus.csproj", "src/BuildingBlocks/GeekStore.MessageBus/"]
COPY ["src/Services/Identity/GeekStore.Identity.Domain/GeekStore.Identity.Domain.csproj", "src/Services/Identity/GeekStore.Identity.Domain/"]
COPY ["src/Services/Identity/GeekStore.Identity.Data/GeekStore.Identity.Data.csproj", "src/Services/Identity/GeekStore.Identity.Data/"]
RUN dotnet restore "src/Services/Identity/GeekStore.Identity.WebAPI/GeekStore.Identity.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Services/Identity/GeekStore.Identity.WebAPI"
RUN dotnet build "GeekStore.Identity.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeekStore.Identity.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeekStore.Identity.WebAPI.dll"]