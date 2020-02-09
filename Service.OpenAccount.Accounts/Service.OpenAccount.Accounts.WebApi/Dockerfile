ARG CONFIGURATION

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
ARG CONFIGURATION
WORKDIR /build
COPY . .
RUN dotnet restore "Service.OpenAccount.Accounts/Service.OpenAccount.Accounts.WebApi"
RUN dotnet build "Service.OpenAccount.Accounts/Service.OpenAccount.Accounts.WebApi" --configuration ${CONFIGURATION} --no-restore

From build AS publish
ARG CONFIGURATION
WORKDIR /build
RUN dotnet publish "Service.OpenAccount.Accounts/Service.OpenAccount.Accounts.WebApi" --configuration ${CONFIGURATION} --no-build --no-restore --output ./publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
COPY --from=publish /build/publish .
ENTRYPOINT ["dotnet", "Service.OpenAccount.Accounts.WebApi.dll"]