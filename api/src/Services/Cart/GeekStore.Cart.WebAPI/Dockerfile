#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Cart/GeekStore.Cart.WebAPI/GeekStore.Cart.WebAPI.csproj", "src/Services/Cart/GeekStore.Cart.WebAPI/"]
COPY ["src/BuildingBlocks/GeekStore.WebApi.Core/GeekStore.WebApi.Core.csproj", "src/BuildingBlocks/GeekStore.WebApi.Core/"]
COPY ["src/BuildingBlocks/GeekStore.Core/GeekStore.Core.csproj", "src/BuildingBlocks/GeekStore.Core/"]
COPY ["src/Services/Cart/GeekStore.Cart.Application/GeekStore.Cart.Application.csproj", "src/Services/Cart/GeekStore.Cart.Application/"]
COPY ["src/Services/Cart/GeekStore.Cart.Domain/GeekStore.Cart.Domain.csproj", "src/Services/Cart/GeekStore.Cart.Domain/"]
COPY ["src/Services/Cart/GeekStore.Cart.Data/GeekStore.Cart.Data.csproj", "src/Services/Cart/GeekStore.Cart.Data/"]
RUN dotnet restore "src/Services/Cart/GeekStore.Cart.WebAPI/GeekStore.Cart.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Services/Cart/GeekStore.Cart.WebAPI"
RUN dotnet build "GeekStore.Cart.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeekStore.Cart.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeekStore.Cart.WebAPI.dll"]