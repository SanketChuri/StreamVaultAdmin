namespace StreamVaultAdmin.Models
{
    public class Series : ContentItem
    {
        public int NumberOfSeasons { get; set; }
        public int TotalEpisodes { get; set; }

        public override string ContentType => "Series";

        public override void UpdateTypeFields(IFormCollection form)
        {
            NumberOfSeasons = int.TryParse(form["NumberOfSeasons"], out int s) ? s : 0;
            TotalEpisodes = int.TryParse(form["TotalEpisodes"], out int e) ? e : 0;
        }

        // Series knows how to validate its own fields
        public override List<string> ValidateTypeFields()
        {
            var errors = new List<string>();

            if (NumberOfSeasons <= 0)
                errors.Add("Number of Seasons must be greater than 0");

            if (TotalEpisodes <= 0)
                errors.Add("Total Episodes must be greater than 0");

            return errors;
        }
    }

    
}