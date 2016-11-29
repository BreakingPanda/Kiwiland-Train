namespace Train.Service.Models
{
    public class Track
    {
        public string From { get; set; }

        public string To { get; set; }

        public int Distance { get; set; }

        public static Track NewTrack(string track)
        {
            return new Track
            {
                From = $"{track[0]}",
                To = $"{track[1]}",
                Distance = int.Parse(track.Substring(2))
            };
        }
    }
}
