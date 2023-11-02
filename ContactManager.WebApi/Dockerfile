FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ContactManager.WebApi/ContactManager.WebApi.csproj", "ContactManager.WebApi/"]
COPY ["ContactManager.Contracts/ContactManager.Contracts.csproj", "ContactManager.Contracts/"]
COPY ["ContactManager.Application/ContactManager.Application.csproj", "ContactManager.Application/"]
COPY ["ContactManager.Domain/ContactManager.Domain.csproj", "ContactManager.Domain/"]
COPY ["ContactManager.Infrastructure/ContactManager.Infrastructure.csproj", "ContactManager.Infrastructure/"]
RUN dotnet restore "ContactManager.WebApi/ContactManager.WebApi.csproj"
COPY . .
WORKDIR "/src/ContactManager.WebApi"
RUN dotnet build "ContactManager.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactManager.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactManager.WebApi.dll"]
