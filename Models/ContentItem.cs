using Microsoft.AspNetCore.Http;

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

        // Each content type returns its own name
        public abstract string ContentType { get; }

        // Shared fields are updated here once, not repeated in every child
        public void UpdateSharedFields(IFormCollection form)
        {
            Title = form["Title"].ToString();
            Description = form["Description"].ToString();
            AgeRating = form["AgeRating"].ToString();
            Genre = form["Genre"].ToString();

            var dateText = form["ReleaseDate"].ToString();
            if (DateTime.TryParse(dateText, out DateTime date))
            {
                ReleaseDate = date;
            }
        }

        // Each child class knows how to update its own specific fields
        public abstract void UpdateTypeFields(IFormCollection form);

        // Validate shared fields — written once for all types
        public List<string> ValidateSharedFields()
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(Title))
                errors.Add("Title is required");

            if (string.IsNullOrEmpty(Description))
                errors.Add("Description is required");

            if (string.IsNullOrEmpty(AgeRating))
                errors.Add("Age Rating is required");

            if (string.IsNullOrEmpty(Genre))
                errors.Add("Genre is required");

            if (ReleaseDate == default)
                errors.Add("Release Date is required");

            return errors;
        }

        // Each child class validates its own specific fields
        public abstract List<string> ValidateTypeFields();
    }
}