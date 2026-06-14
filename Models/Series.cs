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
    }
}