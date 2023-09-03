#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Coupon/GeekStore.Coupon.WebAPI/GeekStore.Coupon.WebAPI.csproj", "src/Services/Coupon/GeekStore.Coupon.WebAPI/"]
COPY ["src/BuildingBlocks/GeekStore.Core/GeekStore.Core.csproj", "src/BuildingBlocks/GeekStore.Core/"]
COPY ["src/BuildingBlocks/GeekStore.WebApi.Core/GeekStore.WebApi.Core.csproj", "src/BuildingBlocks/GeekStore.WebApi.Core/"]
COPY ["src/Services/Coupon/GeekStore.Coupon.Application/GeekStore.Coupon.Application.csproj", "src/Services/Coupon/GeekStore.Coupon.Application/"]
COPY ["src/Services/Coupon/GeekStore.Coupon.Domain/GeekStore.Coupon.Domain.csproj", "src/Services/Coupon/GeekStore.Coupon.Domain/"]
COPY ["src/Services/Coupon/GeekStore.Coupon.Data/GeekStore.Coupon.Data.csproj", "src/Services/Coupon/GeekStore.Coupon.Data/"]
RUN dotnet restore "src/Services/Coupon/GeekStore.Coupon.WebAPI/GeekStore.Coupon.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Services/Coupon/GeekStore.Coupon.WebAPI"
RUN dotnet build "GeekStore.Coupon.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeekStore.Coupon.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeekStore.Coupon.WebAPI.dll"]