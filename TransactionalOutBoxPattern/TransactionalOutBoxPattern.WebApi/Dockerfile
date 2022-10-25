
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TransactionalOutBoxPattern.WebApi/TransactionalOutBoxPattern.WebApi.csproj", "TransactionalOutBoxPattern.WebApi/"]
COPY ["TransactionalOutBoxPattern.Application/TransactionalOutBoxPattern.Application.csproj", "TransactionalOutBoxPattern.Application/"]
COPY ["TransactionalOutBoxPattern.Infrastructure/TransactionalOutBoxPattern.Infrastructure.csproj", "TransactionalOutBoxPattern.Infrastructure/"]
COPY ["TransactionalOutBoxPattern.Domain/TransactionalOutBoxPattern.Domain.csproj", "TransactionalOutBoxPattern.Domain/"]
RUN dotnet restore "TransactionalOutBoxPattern.WebApi/TransactionalOutBoxPattern.WebApi.csproj"
COPY . .
WORKDIR "/src/TransactionalOutBoxPattern.WebApi"
RUN dotnet build "TransactionalOutBoxPattern.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TransactionalOutBoxPattern.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TransactionalOutBoxPattern.WebApi.dll"]