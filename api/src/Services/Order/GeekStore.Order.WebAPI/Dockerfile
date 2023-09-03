#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Order/GeekStore.Order.WebAPI/GeekStore.Order.WebAPI.csproj", "src/Services/Order/GeekStore.Order.WebAPI/"]
COPY ["src/BuildingBlocks/GeekStore.EventSourcing/GeekStore.EventSourcing.csproj", "src/BuildingBlocks/GeekStore.EventSourcing/"]
COPY ["src/BuildingBlocks/GeekStore.Core/GeekStore.Core.csproj", "src/BuildingBlocks/GeekStore.Core/"]
COPY ["src/BuildingBlocks/GeekStore.WebApi.Core/GeekStore.WebApi.Core.csproj", "src/BuildingBlocks/GeekStore.WebApi.Core/"]
COPY ["src/Services/Order/GeekStore.Order.Application/GeekStore.Order.Application.csproj", "src/Services/Order/GeekStore.Order.Application/"]
COPY ["src/Services/Order/GeekStore.Order.Domain/GeekStore.Order.Domain.csproj", "src/Services/Order/GeekStore.Order.Domain/"]
COPY ["src/Services/Order/GeekStore.Order.Data/GeekStore.Order.Data.csproj", "src/Services/Order/GeekStore.Order.Data/"]
RUN dotnet restore "src/Services/Order/GeekStore.Order.WebAPI/GeekStore.Order.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Services/Order/GeekStore.Order.WebAPI"
RUN dotnet build "GeekStore.Order.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeekStore.Order.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeekStore.Order.WebAPI.dll"]