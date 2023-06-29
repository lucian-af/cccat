#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

ENV Conexao="Server=mssql,1433;Database=CCCAT_Frete;User ID=sa;Password=Secret@1234;TrustServerCertificate=true;"
ENV ASPNETCORE_URLS=http://+:5102
EXPOSE 5102

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Cccat.Frete/Cccat.Frete.API/Cccat.Frete.API.csproj", "src/Cccat.Frete/Cccat.Frete.API/"]
COPY ["src/Cccat.Frete/Cccat.Frete.Application/Cccat.Frete.Application.csproj", "src/Cccat.Frete/Cccat.Frete.Application/"]
COPY ["src/Cccat.Frete/Cccat.Frete.Domain/Cccat.Frete.Domain.csproj", "src/Cccat.Frete/Cccat.Frete.Domain/"]
COPY ["src/Cccat.Frete/Cccat.Frete.Infra/Cccat.Frete.Infra.csproj", "src/Cccat.Frete/Cccat.Frete.Infra/"]
RUN dotnet restore "src/Cccat.Frete/Cccat.Frete.API/Cccat.Frete.API.csproj"
COPY . .
WORKDIR "/src/src/Cccat.Frete/Cccat.Frete.API"
RUN dotnet build "Cccat.Frete.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cccat.Frete.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cccat.Frete.API.dll"]