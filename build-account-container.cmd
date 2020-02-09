@ECHO OFF
SETLOCAL
SET VERSION_WEBAPI="1.0"

REM Build Service.OpenAccount.Transactions & Service.OpenAccount.Customers & Service.OpenAccount.Accounts
REM - CONFIGUTATION e.g. Development,Production

REM Build and Run Accounts Web API
docker build -f Service.OpenAccount.Accounts/Service.OpenAccount.Accounts.WebApi/Dockerfile --build-arg CONFIGURATION=release --rm --tag accounts-web-api:%VERSION_WEBAPI% .
docker run -d -p 5004:80 --name service.accounts accounts-web-api:%VERSION_WEBAPI%



