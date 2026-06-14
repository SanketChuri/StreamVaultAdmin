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

        // Load all items from every content type
        private List<ContentItem> LoadAllItems()
        {
            var items = new List<ContentItem>();
            items.AddRange(_db.Movies.ToList());
            items.AddRange(_db.Series.ToList());
            items.AddRange(_db.Audiobooks.ToList());
            items.AddRange(_db.MusicAlbums.ToList());
            return items;
        }

        // Filter by content type if provided
        private List<ContentItem> FilterByType(List<ContentItem> items, string? type)
        {
            if (string.IsNullOrEmpty(type))
                return items;

            if (type == "Movie")      return items.OfType<Movie>().Cast<ContentItem>().ToList();
            if (type == "Series")     return items.OfType<Series>().Cast<ContentItem>().ToList();
            if (type == "Audiobook")  return items.OfType<Audiobook>().Cast<ContentItem>().ToList();
            if (type == "MusicAlbum") return items.OfType<MusicAlbum>().Cast<ContentItem>().ToList();

            return items;
        }

        // Filter by title search if provided
        private List<ContentItem> FilterBySearch(List<ContentItem> items, string? search)
        {
            if (string.IsNullOrEmpty(search))
                return items;

            return items
                .Where(x => x.Title.Contains(search))
                .ToList();
        }

        public List<ContentItem> GetAll(string? type = null, string? search = null)
        {
            var items = LoadAllItems();
            items = FilterByType(items, type);
            items = FilterBySearch(items, search);
            return items;
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

        // Add new item to database
        public void Add(ContentItem item)
        {
            _db.Add(item);
            _db.SaveChanges();
        }

        // Update existing item in database
        public void Update(ContentItem item)
        {
            _db.Update(item);
            _db.SaveChanges();
        }

        // Delete item from database by id
        public void Delete(int id)
        {
            var item = GetById(id);
            if (item == null) return;

            _db.Remove(item);
            _db.SaveChanges();
        }
    }
}