FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Customer.Web/Customer.Web.csproj", "Customer.Web/"]
COPY ["StoreApi/StoreApi.csproj", "StoreApi/"]
COPY ["Identity.Data/Identity.Data.csproj", "Identity.Data/"]
COPY ["Customer.Data/Customer.Data.csproj", "Customer.Data/"]
COPY ["Customer.Services/Customer.Services.csproj", "Customer.Services/"]
RUN dotnet restore "Customer.Web/Customer.Web.csproj"
COPY . .
WORKDIR "/src/Customer.Web"
RUN dotnet build "Customer.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Customer.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customer.Web.dll"]