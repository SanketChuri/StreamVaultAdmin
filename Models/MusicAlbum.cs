namespace StreamVaultAdmin.Models
{
    public class MusicAlbum : ContentItem
    {
        public string Artist { get; set; } = string.Empty;
        public int TrackCount { get; set; }
        public string RecordLabel { get; set; } = string.Empty;

        public override string ContentType => "MusicAlbum";

        public override void UpdateTypeFields(IFormCollection form)
        {
            Artist = form["Artist"].ToString() ?? string.Empty;
            TrackCount = int.TryParse(form["TrackCount"], out int t) ? t : 0;
            RecordLabel = form["RecordLabel"].ToString() ?? string.Empty;
        }
    }
}