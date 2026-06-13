namespace StreamVaultAdmin.Models
{
    public class MusicAlbum : ContentItem
    {
        public string Artist { get; set; } = string.Empty;
        public int TrackCount { get; set; }
        public string RecordLabel { get; set; } = string.Empty;
    }
}