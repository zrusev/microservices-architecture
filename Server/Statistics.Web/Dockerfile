FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Statistics.Web/Statistics.Web.csproj", "Statistics.Web/"]
COPY ["Statistics.Data/Statistics.Data.csproj", "Statistics.Data/"]
COPY ["StoreApi/StoreApi.csproj", "StoreApi/"]
COPY ["Identity.Data/Identity.Data.csproj", "Identity.Data/"]
COPY ["Statistics.Services/Statistics.Services.csproj", "Statistics.Services/"]
RUN dotnet restore "Statistics.Web/Statistics.Web.csproj"
COPY . .
WORKDIR "/src/Statistics.Web"
RUN dotnet build "Statistics.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Statistics.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Statistics.Web.dll"]