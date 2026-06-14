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
    }
}