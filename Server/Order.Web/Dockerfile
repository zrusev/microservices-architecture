FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Order.Web/Order.Web.csproj", "Order.Web/"]
COPY ["StoreApi/StoreApi.csproj", "StoreApi/"]
COPY ["Identity.Data/Identity.Data.csproj", "Identity.Data/"]
COPY ["Order.Services/Order.Services.csproj", "Order.Services/"]
COPY ["Order.Data/Order.Data.csproj", "Order.Data/"]
RUN dotnet restore "Order.Web/Order.Web.csproj"
COPY . .
WORKDIR "/src/Order.Web"
RUN dotnet build "Order.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Web.dll"]