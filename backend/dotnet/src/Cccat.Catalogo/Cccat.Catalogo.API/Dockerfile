#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

ENV ASPNETCORE_URLS=http://+:5101
EXPOSE 5101

ENV Conexao="Server=172.23.0.100,1433;Database=CCCAT_Catalogo;User ID=sa;Password=Secret@1234;TrustServerCertificate=true;"

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Cccat.Catalogo/Cccat.Catalogo.API/Cccat.Catalogo.API.csproj", "src/Cccat.Catalogo/Cccat.Catalogo.API/"]
COPY ["src/Cccat.Catalogo/Cccat.Catalogo.Application/Cccat.Catalogo.Application.csproj", "src/Cccat.Catalogo/Cccat.Catalogo.Application/"]
COPY ["src/Cccat.Catalogo/Cccat.Catalogo.Domain/Cccat.Catalogo.Domain.csproj", "src/Cccat.Catalogo/Cccat.Catalogo.Domain/"]
COPY ["src/Cccat.Catalogo/Cccat.Catalogo.Infra/Cccat.Catalogo.Infra.csproj", "src/Cccat.Catalogo/Cccat.Catalogo.Infra/"]
RUN dotnet restore "src/Cccat.Catalogo/Cccat.Catalogo.API/Cccat.Catalogo.API.csproj"
COPY . .
WORKDIR "/src/src/Cccat.Catalogo/Cccat.Catalogo.API"
RUN dotnet build "Cccat.Catalogo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cccat.Catalogo.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cccat.Catalogo.API.dll"]