#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Product/GeekStore.Product.WebAPI/GeekStore.Product.WebAPI.csproj", "src/Services/Product/GeekStore.Product.WebAPI/"]
COPY ["src/BuildingBlocks/GeekStore.EventSourcing/GeekStore.EventSourcing.csproj", "src/BuildingBlocks/GeekStore.EventSourcing/"]
COPY ["src/BuildingBlocks/GeekStore.Core/GeekStore.Core.csproj", "src/BuildingBlocks/GeekStore.Core/"]
COPY ["src/BuildingBlocks/GeekStore.WebApi.Core/GeekStore.WebApi.Core.csproj", "src/BuildingBlocks/GeekStore.WebApi.Core/"]
COPY ["src/Services/Product/GeekStore.Product.Data/GeekStore.Product.Data.csproj", "src/Services/Product/GeekStore.Product.Data/"]
COPY ["src/Services/Product/GeekStore.Product.Application/GeekStore.Product.Application.csproj", "src/Services/Product/GeekStore.Product.Application/"]
COPY ["src/Services/Product/GeekStore.Product.Domain/GeekStore.Product.Domain.csproj", "src/Services/Product/GeekStore.Product.Domain/"]
RUN dotnet restore "src/Services/Product/GeekStore.Product.WebAPI/GeekStore.Product.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Services/Product/GeekStore.Product.WebAPI"
RUN dotnet build "GeekStore.Product.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeekStore.Product.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeekStore.Product.WebAPI.dll"]