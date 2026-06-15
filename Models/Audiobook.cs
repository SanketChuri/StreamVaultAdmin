namespace StreamVaultAdmin.Models
{
    public class Audiobook : ContentItem
    {
        public string Author { get; set; } = string.Empty;
        public string Narrator { get; set; } = string.Empty;
        public int Duration { get; set; }

        public override string ContentType => "Audiobook";

        public override void UpdateTypeFields(IFormCollection form)
        {
            Author = form["Author"].ToString() ?? string.Empty;
            Narrator = form["Narrator"].ToString() ?? string.Empty;
            Duration = int.TryParse(form["Duration"], out int d) ? d : 0;
        }

        // Audiobook knows how to validate its own fields
        public override List<string> ValidateTypeFields()
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(Author))
                errors.Add("Author is required");

            if (string.IsNullOrEmpty(Narrator))
                errors.Add("Narrator is required");

            if (Duration <= 0)
                errors.Add("Duration must be greater than 0");

            return errors;
        }

        public override string GetDetails()
        {
            return Author + " | " + Narrator + " | " + Duration + " mins";
        }
    }
}