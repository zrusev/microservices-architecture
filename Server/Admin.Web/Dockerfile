FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Admin.Web/Admin.Web.csproj", "Admin.Web/"]
COPY ["StoreApi/StoreApi.csproj", "StoreApi/"]
COPY ["Identity.Data/Identity.Data.csproj", "Identity.Data/"]
COPY ["Admin.Services/Admin.Services.csproj", "Admin.Services/"]
RUN dotnet restore "Admin.Web/Admin.Web.csproj"
COPY . .
WORKDIR "/src/Admin.Web"
RUN dotnet build "Admin.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Admin.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Admin.Web.dll"]