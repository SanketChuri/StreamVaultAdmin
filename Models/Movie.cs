namespace StreamVaultAdmin.Models
{
    public class Movie : ContentItem
    {
        public int Duration { get; set; }
        public string Director { get; set; } = string.Empty;
    }
}