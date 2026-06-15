# StreamVault Admin

A simple internal admin tool to manage the StreamVault streaming platform catalogue. Built as part of a technical test using ASP.NET Core MVC.

## How to Run

1. Clone the repository
2. Open a terminal and navigate to the project folder
3. Run this command:

```bash
dotnet run
```

4. Open your browser and go to `http://localhost:5245/Catalogue`

No database setup needed. The database is created and filled with sample data automatically when you run the app for the first time.

## How the Database Works

I used SQLite with Entity Framework Core. When the app starts for the first time:

1. EF Core checks if the database file exists.
2. If it does not exist, it creates it automatically.
3. The seeder then checks if the catalogue has any data.
4. If it is empty, it adds 8 sample items (2 of each content type).
5. Next time you run the app, the seeder sees existing data and skips.

So you can clone the repo, run it, and see a working catalogue straight away.

## My Design Decisions

### Inheritance

All four content types share common fields like Title, Description, Release Date, Age Rating and Genre. I put these in an abstract base class called ContentItem so I only write them once.

Each type then adds its own specific fields:

1. Movie adds Duration and Director
2. Series adds NumberOfSeasons and TotalEpisodes
3. Audiobook adds Author, Narrator and Duration
4. MusicAlbum adds Artist, TrackCount and RecordLabel

I also added three abstract members to the base class:

1. ContentType — each class returns its own type name like "Movie" or "Series"
2. UpdateTypeFields — each class updates its own fields from a submitted form
3. ValidateTypeFields — each class validates its own fields

This means the controller never needs to check what type an item is. It just calls these methods and the right implementation runs automatically. If someone adds a fifth content type in the future, the controller and service do not need to change at all.

### Why EF Core over Dapper or ADO.NET

I chose EF Core for three main reasons:

1. The database needs to be created automatically on first run. EF Core does this in one line using EnsureCreated()
2. EF Core handles the inheritance mapping automatically using Table Per Hierarchy. All four types go into one table called ContentItems with a Discriminator column that tells EF Core which type each row is
3. Everything is written in C# so there are no raw SQL strings that could have typos

The tradeoff with Table Per Hierarchy is that some columns will be NULL for rows that do not use them. For example a Series row will have NULL in the Director column. For a catalogue this size that is completely fine.

### Separation of Concerns

1. Controllers are kept thin. They only receive requests, call the service and return views
2. CatalogueService handles all data access and filtering
3. Each model class handles its own field updates and validation
4. The seeder is in its own class in the Data folder

## What I Would Add Next

1. Pagination so the list does not get too long as the catalogue grows
2. A detail view to see all fields of an item without going into edit mode
3. Unit tests for the service layer especially the filtering and search logic
4. Move the database connection string into appsettings.json instead of hardcoding it in Program.cs

## Tech Stack (Technology and Purpose)

1. ASP.NET Core MVC (.NET 10) - Web framework
2. Entity Framework Core - Database management
3. SQLite - Database
4. Bootstrap - Basic styling


## Project Structure

StreamVaultAdmin/
├── Controllers/
│   └── CatalogueController.cs   ← handles all web requests
├── Data/
│   ├── AppDbContext.cs          ← database setup
│   └── DbSeeder.cs              ← sample data on first run
├── Models/
│   ├── ContentItem.cs           ← base class
│   ├── Movie.cs                 ← inherits ContentItem
│   ├── Series.cs                ← inherits ContentItem
│   ├── Audiobook.cs             ← inherits ContentItem
│   └── MusicAlbum.cs            ← inherits ContentItem
├── Services/
│   └── CatalogueService.cs      ← all business logic
├── Views/
│   └── Catalogue/
│       ├── Index.cshtml         ← list page
│       ├── Create.cshtml        ← add new content
│       ├── Edit.cshtml          ← edit content
│       └── Delete.cshtml        ← confirm delete
└── Program.cs                   ← app startup


## Features

1. View all catalogue items in a table.
2. Filter by content type (Movie, Series, Audiobook, Music Album).
3. Search by title (case insensitive).
4. Sort by Title, Type or Release Date.
5. Add new content of any type.
6. Edit existing content.
7. Delete content with confirmation.
8. Server side validation with clear error messages.
9. Sample data loaded automatically on first run.

## Known Limitations

1. IDs are not globally unique across content types because each DbSet has its own ID sequence. I worked around this by passing the content type in the URL alongside the ID.
2. No authentication — the test spec says to assume the user is already logged in.
3. Search only works on Title — could be extended to search Description and Genre.

