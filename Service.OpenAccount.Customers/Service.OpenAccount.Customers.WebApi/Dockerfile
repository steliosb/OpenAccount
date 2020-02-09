FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /build
COPY . .
RUN dotnet restore "Service.OpenAccount.Customers/Service.OpenAccount.Customers.WebApi"
RUN dotnet build "Service.OpenAccount.Customers/Service.OpenAccount.Customers.WebApi" --no-restore

From build AS publish
WORKDIR /build
RUN dotnet publish "Service.OpenAccount.Customers/Service.OpenAccount.Customers.WebApi" --no-build --no-restore --output ./publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
COPY --from=publish /build/publish .
ENTRYPOINT ["dotnet", "Service.OpenAccount.Customers.WebApi.dll"]