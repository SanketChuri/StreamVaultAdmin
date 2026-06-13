namespace StreamVaultAdmin.Models
{
    public class Audiobook : ContentItem
    {
        public string Author { get; set; } = string.Empty;
        public string Narrator { get; set; } = string.Empty;
        public int Duration { get; set; }
    }
}