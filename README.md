# Mailing List

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)

## General info
Mailing list application using ASP .NET Core 3.1
	
## Technologies
Project is created with:
* ASP .NET Core 3.1
* MediatR to use CQRS
	
## Setup

To run Mailing list server application:

### Requirements:
* .NET Core 3.1 installed on computer
* SQL Server

```
Set connection string to your local database in appsettings.Development.json
```

```
Use update-database command to create your local database instance with applying migrations:
https://docs.microsoft.com/pl-pl/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
```

```
Run app:
https://docs.microsoft.com/pl-pl/dotnet/core/tools/dotnet-run
```

```
Swagger should be open in new instance of your browser
```

![Alt text](images/SwaggerView.jpg?raw=true "Swagger")

```
After registration token should be places in pattern as we can see on image.
```

![Alt text](images/TokenPattern.jpg?raw=true "Swagger")
