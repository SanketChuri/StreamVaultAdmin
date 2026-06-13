using Microsoft.EntityFrameworkCore;
using StreamVaultAdmin.Models;

namespace StreamVaultAdmin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Audiobook> Audiobooks { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<MusicAlbum> MusicAlbums { get; set; }
    }
}