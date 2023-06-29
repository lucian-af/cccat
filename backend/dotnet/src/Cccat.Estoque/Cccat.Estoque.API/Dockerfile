#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

ENV Conexao="Server=172.23.0.100,1433;Database=CCCAT_Estoque;User ID=sa;Password=Secret@1234;TrustServerCertificate=true;"
ENV ASPNETCORE_URLS=http://+:5105
EXPOSE 5105

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Cccat.Estoque/Cccat.Estoque.API/Cccat.Estoque.API.csproj", "src/Cccat.Estoque/Cccat.Estoque.API/"]
COPY ["src/Cccat.Estoque/Cccat.Estoque.Application/Cccat.Estoque.Application.csproj", "src/Cccat.Estoque/Cccat.Estoque.Application/"]
COPY ["src/Cccat.Estoque/Cccat.Estoque.Domain/Cccat.Estoque.Domain.csproj", "src/Cccat.Estoque/Cccat.Estoque.Domain/"]
COPY ["src/Cccat.Estoque/Cccat.Estoque.BackgroundTask/Cccat.Estoque.BackgroundTask.csproj", "src/Cccat.Estoque/Cccat.Estoque.BackgroundTask/"]
COPY ["src/Cccat.Estoque/Cccat.Estoque.Infra/Cccat.Estoque.Infra.csproj", "src/Cccat.Estoque/Cccat.Estoque.Infra/"]
RUN dotnet restore "src/Cccat.Estoque/Cccat.Estoque.API/Cccat.Estoque.API.csproj"
COPY . .
WORKDIR "/src/src/Cccat.Estoque/Cccat.Estoque.API"
RUN dotnet build "Cccat.Estoque.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cccat.Estoque.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cccat.Estoque.API.dll"]