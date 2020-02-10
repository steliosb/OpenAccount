# Service.OpenAccount

Open Account v1 is a service that can be used to open a new account for a customer who execute a transaction with specific amount

## Stack
- .NET Core 3.1.
- ASP.NET Web-API
- Mailkit (cross-platform .NET library for IMAP)

## Installation

 1. Install [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) SDK on your local machine
 2. Download the repo through [github](https://github.com/steliosb/Service.OpenAccount) or simply
```bash
git clone https://github.com/steliosb/Service.OpenAccount.git
```
 3. Run the `docker-compose` file (using PowerShell) that resides in the top-level directory of the solution.

> **Note:** *Docker-compose will run 3 services on release enviroment*


## Usage
A useful API specification can be found via swagger tool here:

Service.Customer
 - [local](http://localhost:5004/swagger)
 Service.Account
 - [local](http://localhost:5002/swagger)
 Service.Transaction
 - [local](http://localhost:5000/swagger)

## Support
sbalampanis@gmail.com

## Contributing
Pull requests are welcome. For major changes, please open an ticket first to discuss what you would like to change.

### Author and acknowledgment
*Stelios Balampanis*
