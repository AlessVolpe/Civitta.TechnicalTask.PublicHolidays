# Civitta.TechnicalTask.PublicHolidays

This is my attempt at Civitta's recruitment itinerary technical task. It's a web application built using ASP.NET CORE in .NET 8, it has two different services and tries to use XUnit tests for testing.

## Main issues

- DB doesn't store anything, being used to mostly work on preexisting Databases there's probably something wrong in the Model first approach that I took.
- The application should it's currently deployed as an Azure App container, which for some reason doesn't show any page besides the one in this ![picture]([https://example.com/path/to/image.png](https://imgur.com/a/pfPAux9)).
- The tests are definetly lackluster.
- The ```GetMaximumFreeDays``` endpoint's business logic isn't perfect.

## Local Deploy

Clone the project on your local machine by typing the following command in your shell:

```bash
  git clone https://github.com/AlessVolpe/Civitta.TechnicalTask.PublicHolidays.git
```

Open the solution using Visual Studio 2022's latest version (shouldn't be a problem but better safe than sorry).
In the top menu you can find Tools > NuGet Package Manager > Package Manager Console, to open a little console usually on Visual Studio's bottom panel. 
In the newly opened console you can type the command ```Update-Database``` to apply the migrations and then launch the app using the "Run without debug" button (```CRTL+F5```), this should open the Swagger UI.
Further instruction are documented over the UI.

## Over the web

This ![link](https://civitta-technical-task-app.blacksky-fd9c8caa.australiaeast.azurecontainerapps.io/swagger) should guide you to the swagger ui page that I published, if only it didn't show the same page no matter all the changes.

## Requirements for local deploy
- Visual Studio 2022:latest
- Docker:latest
- Any web browser
