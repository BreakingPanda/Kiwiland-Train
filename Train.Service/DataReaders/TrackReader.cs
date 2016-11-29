using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Train.Service.Models;

namespace Train.Service.DataReaders
{
    public class TrackReader : ITrackReader
    {
        public async Task<IEnumerable<Track>> ReadFromFile(string filepath)
        {
            using (var stream = File.OpenRead(filepath))
            {
                var bytes = new byte[stream.Length];

                await stream.ReadAsync(bytes, 0, (int)stream.Length);

                string content = Encoding.UTF8.GetString(bytes);

                return Transform(content);
            }
        }

        private IEnumerable<Track> Transform(string content)
        {
            if (content.StartsWith("\ufeff"))
            {
                content = content.Substring(1);
            }

            var tracks = new List<Track>();

            foreach (string track in content.Split(new [] {','}, StringSplitOptions.RemoveEmptyEntries))
            {
                tracks.Add(Track.NewTrack(track));
            }

            return tracks;
        }
    }
}