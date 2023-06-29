#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

ENV Conexao="Server=mssql,1433;Database=CCCAT_Autenticacao;User ID=sa;Password=Secret@1234;TrustServerCertificate=true;"
ENV ASPNETCORE_URLS=http://+:5104

EXPOSE 5104

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Cccat.Autenticacao/Cccat.Autenticacao.API/Cccat.Autenticacao.API.csproj", "src/Cccat.Autenticacao/Cccat.Autenticacao.API/"]
COPY ["src/Cccat.Autenticacao/Cccat.Autenticacao.Application/Cccat.Autenticacao.Application.csproj", "src/Cccat.Autenticacao/Cccat.Autenticacao.Application/"]
COPY ["src/Cccat.Autenticacao/Cccat.Autenticacao.Domain/Cccat.Autenticacao.Domain.csproj", "src/Cccat.Autenticacao/Cccat.Autenticacao.Domain/"]
COPY ["src/Cccat.Autenticacao/Cccat.Autenticacao.Infra/Cccat.Autenticacao.Infra.csproj", "src/Cccat.Autenticacao/Cccat.Autenticacao.Infra/"]
RUN dotnet restore "src/Cccat.Autenticacao/Cccat.Autenticacao.API/Cccat.Autenticacao.API.csproj"
COPY . .
WORKDIR "/src/src/Cccat.Autenticacao/Cccat.Autenticacao.API"
RUN dotnet build "Cccat.Autenticacao.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cccat.Autenticacao.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cccat.Autenticacao.API.dll"]