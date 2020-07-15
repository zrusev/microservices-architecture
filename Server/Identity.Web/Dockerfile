FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Identity.Web/Identity.Web.csproj", "Identity.Web/"]
COPY ["StoreApi/StoreApi.csproj", "StoreApi/"]
COPY ["Identity.Data/Identity.Data.csproj", "Identity.Data/"]
COPY ["Identity.Services/Identity.Services.csproj", "Identity.Services/"]
RUN dotnet restore "Identity.Web/Identity.Web.csproj"
COPY . .
WORKDIR "/src/Identity.Web"
RUN dotnet build "Identity.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Web.dll"]