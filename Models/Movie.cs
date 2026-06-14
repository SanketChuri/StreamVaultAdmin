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
    }
}