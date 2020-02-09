FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /build
COPY . .
RUN dotnet restore "Service.OpenAccount.Transactions/Service.OpenAccount.Transactions.WebApi"
RUN dotnet build "Service.OpenAccount.Transactions/Service.OpenAccount.Transactions.WebApi" --no-restore

From build AS publish
WORKDIR /build
RUN dotnet publish "Service.OpenAccount.Transactions/Service.OpenAccount.Transactions.WebApi" --no-build --no-restore --output ./publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
COPY --from=publish /build/publish .
ENTRYPOINT ["dotnet", "Service.OpenAccount.Transactions.WebApi.dll"]