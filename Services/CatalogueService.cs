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
            var allItems = new List<ContentItem>();

            allItems.AddRange(_db.Movies.ToList());
            allItems.AddRange(_db.Series.ToList());
            allItems.AddRange(_db.Audiobooks.ToList());
            allItems.AddRange(_db.MusicAlbums.ToList());

            if (!string.IsNullOrEmpty(type))
            {
                allItems = type switch
                {
                    "Movie"      => allItems.OfType<Movie>().Cast<ContentItem>().ToList(),
                    "Series"     => allItems.OfType<Series>().Cast<ContentItem>().ToList(),
                    "Audiobook"  => allItems.OfType<Audiobook>().Cast<ContentItem>().ToList(),
                    "MusicAlbum" => allItems.OfType<MusicAlbum>().Cast<ContentItem>().ToList(),
                    _            => allItems
                };
            }

            if (!string.IsNullOrEmpty(search))
            {
                allItems = allItems
                    .Where(x => x.Title.Contains(search,
                        StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return allItems;
        }

        public ContentItem? GetById(int id)
        {
            ContentItem? item = null;

            item = _db.Movies.FirstOrDefault(x => x.Id == id);
            if (item != null) return item;

            item = _db.Series.FirstOrDefault(x => x.Id == id);
            if (item != null) return item;

            item = _db.Audiobooks.FirstOrDefault(x => x.Id == id);
            if (item != null) return item;

            return _db.MusicAlbums.FirstOrDefault(x => x.Id == id);
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