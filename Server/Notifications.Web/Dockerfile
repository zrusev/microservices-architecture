FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Notifications.Web/Notifications.Web.csproj", "Notifications.Web/"]
COPY ["StoreApi/StoreApi.csproj", "StoreApi/"]
COPY ["Identity.Data/Identity.Data.csproj", "Identity.Data/"]
RUN dotnet restore "Notifications.Web/Notifications.Web.csproj"
COPY . .
WORKDIR "/src/Notifications.Web"
RUN dotnet build "Notifications.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notifications.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notifications.Web.dll"]