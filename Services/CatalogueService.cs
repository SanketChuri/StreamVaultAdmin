using Microsoft.EntityFrameworkCore;
using StreamVaultAdmin.Data;
using StreamVaultAdmin.Models;

namespace StreamVaultAdmin.Services
{
    public class CatalogueService
    {
        private readonly AppDbContext _db;

        public CatalogueService(AppDbContext db)
        {
            _db = db;
        }

        public List<ContentItem> GetAll(string? type = null, string? search = null)
        {
            IQueryable<ContentItem> query = _db.Movies
                .Cast<ContentItem>()
                .Union(_db.Series.Cast<ContentItem>())
                .Union(_db.Audiobooks.Cast<ContentItem>())
                .Union(_db.MusicAlbums.Cast<ContentItem>());

            if (!string.IsNullOrEmpty(type))
            {
                query = type switch
                {
                    "Movie"      => _db.Movies.Cast<ContentItem>(),
                    "Series"     => _db.Series.Cast<ContentItem>(),
                    "Audiobook"  => _db.Audiobooks.Cast<ContentItem>(),
                    "MusicAlbum" => _db.MusicAlbums.Cast<ContentItem>(),
                    _            => query
                };
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Title.Contains(search));
            }

            return query.ToList();
        }

        public ContentItem? GetById(int id)
        {
            return _db.Movies.Cast<ContentItem>()
                .Union(_db.Series.Cast<ContentItem>())
                .Union(_db.Audiobooks.Cast<ContentItem>())
                .Union(_db.MusicAlbums.Cast<ContentItem>())
                .FirstOrDefault(x => x.Id == id);
        }

        public void Add(ContentItem item)
        {
            _db.Add(item);
            _db.SaveChanges();
        }

        public void Update(ContentItem item)
        {
            _db.Update(item);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = GetById(id);
            if (item != null)
            {
                _db.Remove(item);
                _db.SaveChanges();
            }
        }
    }
}