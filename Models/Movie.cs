namespace StreamVaultAdmin.Models
{
    public class Movie : ContentItem
    {
        public int Duration { get; set; }
        public string Director { get; set; } = string.Empty;

        public override string ContentType => "Movie";

        public override void UpdateTypeFields(IFormCollection form)
        {
            Duration = int.TryParse(form["Duration"], out int d) ? d : 0;
            Director = form["Director"].ToString() ?? string.Empty;
        }

        // Movie knows how to validate its own fields
        public override List<string> ValidateTypeFields()
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(Director))
                errors.Add("Director is required");

            if (Duration <= 0)
                errors.Add("Duration must be greater than 0");

            return errors;
        }

        public override string GetDetails()
        {
            return Duration + " mins | " + Director;
        }   
    }
}