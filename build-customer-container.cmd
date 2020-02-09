@ECHO OFF
SETLOCAL
SET VERSION_WEBAPI="1.0"

REM Build Service.OpenAccount.Transactions & Service.OpenAccount.Customers & Service.OpenAccount.Accounts
REM - CONFIGUTATION e.g. Development,Production

REM Build and Run Customers Web API
docker build -f Service.OpenAccount.Customers/Service.OpenAccount.Customers.WebApi/Dockerfile --build-arg CONFIGURATION=release --rm --tag customers-web-api:%VERSION_WEBAPI% .
docker run -d -p  5003:80 --name service.customers customers-web-api:%VERSION_WEBAPI%




