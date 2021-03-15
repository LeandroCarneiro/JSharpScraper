# VetScraper
.NET Core Scraper using Selenium WebDriver

## Architecture

- VetScraper: Console Application that runs the scraper script to grab the data and store it in the database.
- VetScraper.API: Web API (REST) with one resource called /vetclinics that returns the list of clinics paginated in JSON.
- VetScraper.DataCollector: Class Library that contains all the Selenium dependencies and DOM manipulation to retrieve items and transform them into our objects.
- VetScraper.Domain: Class Library that just contains the VetClinic entity.
- VetScraper.Repository: A simple repository that connects to MongoDB to store and retrieve data.

## The flow
- Run the Console Application to collect the data. This Console application could be converted into a Service, an Azure Function, a background process that would be accessing the website and grabbing the data.
- Data is collected and stored in a Mongo database.
- A Web API the Mongo Database and returns paginated results in JSON.
