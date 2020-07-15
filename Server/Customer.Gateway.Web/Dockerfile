FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Customer.Gateway.Web/Customer.Gateway.Web.csproj", "Customer.Gateway.Web/"]
COPY ["StoreApi/StoreApi.csproj", "StoreApi/"]
COPY ["Identity.Data/Identity.Data.csproj", "Identity.Data/"]
COPY ["Customer.Gateway.Services/Customer.Gateway.Services.csproj", "Customer.Gateway.Services/"]
RUN dotnet restore "Customer.Gateway.Web/Customer.Gateway.Web.csproj"
COPY . .
WORKDIR "/src/Customer.Gateway.Web"
RUN dotnet build "Customer.Gateway.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Customer.Gateway.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customer.Gateway.Web.dll"]