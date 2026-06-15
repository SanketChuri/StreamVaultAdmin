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

        // MusicAlbum knows how to validate its own fields
        public override List<string> ValidateTypeFields()
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(Artist))
                errors.Add("Artist is required");

            if (TrackCount <= 0)
                errors.Add("Track Count must be greater than 0");

            if (string.IsNullOrEmpty(RecordLabel))
                errors.Add("Record Label is required");

            return errors;
        }

        public override string GetDetails()
        {
            return Artist + " | " + TrackCount + " tracks";
        }
    }
}