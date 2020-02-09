@ECHO OFF
SETLOCAL
SET VERSION_WEBAPI="1.0"

REM Build Service.OpenAccount.Transactions & Service.OpenAccount.Customers & Service.OpenAccount.Accounts
REM - CONFIGUTATION e.g. Development,Production

REM Build and Run Transactions Web API
docker build -f Service.OpenAccount.Transactions/Service.OpenAccount.Transactions.WebApi/Dockerfile --build-arg CONFIGURATION=release --rm --tag transactions-web-api:%VERSION_WEBAPI% .
docker run -d -p 5002:80 --name service.transactions transactions-web-api:%VERSION_WEBAPI%



