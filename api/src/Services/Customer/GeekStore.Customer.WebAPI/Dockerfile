#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Customer/GeekStore.Customer.WebAPI/GeekStore.Customer.WebAPI.csproj", "src/Services/Customer/GeekStore.Customer.WebAPI/"]
COPY ["src/BuildingBlocks/GeekStore.EventSourcing/GeekStore.EventSourcing.csproj", "src/BuildingBlocks/GeekStore.EventSourcing/"]
COPY ["src/BuildingBlocks/GeekStore.Core/GeekStore.Core.csproj", "src/BuildingBlocks/GeekStore.Core/"]
COPY ["src/BuildingBlocks/GeekStore.MessageBus/GeekStore.MessageBus.csproj", "src/BuildingBlocks/GeekStore.MessageBus/"]
COPY ["src/BuildingBlocks/GeekStore.WebApi.Core/GeekStore.WebApi.Core.csproj", "src/BuildingBlocks/GeekStore.WebApi.Core/"]
COPY ["src/Services/Customer/GeekStore.Customer.Application/GeekStore.Customer.Application.csproj", "src/Services/Customer/GeekStore.Customer.Application/"]
COPY ["src/Services/Customer/GeekStore.Customer.Domain/GeekStore.Customer.Domain.csproj", "src/Services/Customer/GeekStore.Customer.Domain/"]
COPY ["src/Services/Customer/GeekStore.Customer.Data/GeekStore.Customer.Data.csproj", "src/Services/Customer/GeekStore.Customer.Data/"]
RUN dotnet restore "src/Services/Customer/GeekStore.Customer.WebAPI/GeekStore.Customer.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Services/Customer/GeekStore.Customer.WebAPI"
RUN dotnet build "GeekStore.Customer.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeekStore.Customer.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeekStore.Customer.WebAPI.dll"]