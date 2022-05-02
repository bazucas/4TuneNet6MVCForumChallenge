﻿# 4Tune Engineering Fullstack Challenge

## Challenge details

Design an ASP.NET MVC Razor Application to be deployed on IIS using entity
framework and Microsoft SQL Server. A user log's with user and password. A user can
create a topic in a public forum. The topic must contain a title, a full text description, a
creator, and a creation date. Topics can only be edited by the creator of the topic. For
front-end use bootstrap and any other package you would like.

## Requirements

This solution was developed in AspNet 6 MVC, which needs to have installed in your machine the Microsoft .NET 6.0 [Runtime/SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0), as well as [Microsoft SQL Server](https://www.microsoft.com/en-gb/sql-server/sql-server-downloads),
without these dependencies, it won't work.

## Getting this repo

Use PowerShell or bash terminal to clone this repo to your local machine.  
Pick a folder and run this command on the terminal:  

```bash
git clone https://github.com/bazucas/4TuneNet6MVCForumChallenge.git
```

## Setting up the application

After downloading the solution, navigate to the appsettings.json inside the Forum.Web project, and update the database connection string with your own.

```bash
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=4Tune;Trusted_Connection=True"
  }
```

Open a new terminal and navigate to the root of the solution and execute the following commands:

```bash
dotnet restore
dotnet build
```

Everything should run smoothly.

After restoring the dependencies and building the solution, go to the terminal and navigate to the root of the Forum.Web project and run:  

For development mode

```bash
dotnet run --launch-profile Dev
```

For production mode

```bash
dotnet run --launch-profile Prod
```
You should see in the terminal the migrations being executed, creating a new database, and persisting the mock data from the seed json files. 

## Usage

Open the browser and go to [https://localhost:44301/](https://localhost:44301/). You should see the home screen with a logo and, on top, the description of the execution environment.

The execution of the application is quite simple. You can go to the Main/Home screen, Forum, Register and Login.
You can only manage the topics you created, so you need to be registered and logged in. You can't change or delete other users' topics.

You can use the three mock users:

* _jack@gmail.com_
* _sarah@gmail.com_
* _john@gmail.com_

Password = Pa$$w0rd

## Warning

Not all unit tests and exception handling was done, due to the fact that is was not required, and may consume some time.
A few were implemented to show how it works and can be done.

## License

[MIT](https://choosealicense.com/licenses/mit/)
