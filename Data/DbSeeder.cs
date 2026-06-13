using StreamVaultAdmin.Models;

namespace StreamVaultAdmin.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if (db.Movies.Any() || db.Series.Any() || 
                db.Audiobooks.Any() || db.MusicAlbums.Any())
            {
                return;
            }

            db.Movies.AddRange(
                new Movie
                {
                    Title = "Inception",
                    Description = "A thief who steals corporate secrets through dream-sharing technology.",
                    ReleaseDate = new DateTime(2010, 7, 16),
                    AgeRating = "12A",
                    Genre = "Sci-Fi",
                    Duration = 148,
                    Director = "Christopher Nolan"
                },
                new Movie
                {
                    Title = "The Dark Knight",
                    Description = "Batman faces the Joker, a criminal mastermind who wants to plunge Gotham into anarchy.",
                    ReleaseDate = new DateTime(2008, 7, 18),
                    AgeRating = "12A",
                    Genre = "Action",
                    Duration = 152,
                    Director = "Christopher Nolan"
                }
            );

            db.Series.AddRange(
                new Series
                {
                    Title = "Breaking Bad",
                    Description = "A chemistry teacher turned methamphetamine producer.",
                    ReleaseDate = new DateTime(2008, 1, 20),
                    AgeRating = "18",
                    Genre = "Crime",
                    NumberOfSeasons = 5,
                    TotalEpisodes = 62
                },
                new Series
                {
                    Title = "The Office",
                    Description = "A mockumentary about everyday office life.",
                    ReleaseDate = new DateTime(2005, 3, 24),
                    AgeRating = "12",
                    Genre = "Comedy",
                    NumberOfSeasons = 9,
                    TotalEpisodes = 201
                }
            );

            db.Audiobooks.AddRange(
                new Audiobook
                {
                    Title = "Harry Potter and the Philosophers Stone",
                    Description = "A young boy discovers he is a wizard on his 11th birthday.",
                    ReleaseDate = new DateTime(1997, 6, 26),
                    AgeRating = "PG",
                    Genre = "Fantasy",
                    Author = "J.K. Rowling",
                    Narrator = "Stephen Fry",
                    Duration = 498
                },
                new Audiobook
                {
                    Title = "Atomic Habits",
                    Description = "A guide to building good habits and breaking bad ones.",
                    ReleaseDate = new DateTime(2018, 10, 16),
                    AgeRating = "U",
                    Genre = "Self Help",
                    Author = "James Clear",
                    Narrator = "James Clear",
                    Duration = 312
                }
            );

            db.MusicAlbums.AddRange(
                new MusicAlbum
                {
                    Title = "Thriller",
                    Description = "The best selling album of all time.",
                    ReleaseDate = new DateTime(1982, 11, 30),
                    AgeRating = "U",
                    Genre = "Pop",
                    Artist = "Michael Jackson",
                    TrackCount = 9,
                    RecordLabel = "Epic Records"
                },
                new MusicAlbum
                {
                    Title = "Abbey Road",
                    Description = "The eleventh studio album by The Beatles.",
                    ReleaseDate = new DateTime(1969, 9, 26),
                    AgeRating = "U",
                    Genre = "Rock",
                    Artist = "The Beatles",
                    TrackCount = 17,
                    RecordLabel = "Apple Records"
                }
            );

            db.SaveChanges();
        }
    }
}