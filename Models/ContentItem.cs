namespace StreamVaultAdmin.Models
{
    public abstract class ContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string AgeRating { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;

        public void UpdateSharedFields(IFormCollection form)
        {
            Title = form["Title"];
            Description = form["Description"];
            ReleaseDate = DateTime.Parse(form["ReleaseDate"]);
            AgeRating = form["AgeRating"];
            Genre = form["Genre"];
        }

        public abstract void UpdateTypeFields(IFormCollection form);
        public abstract string ContentType { get; }
    }
}