using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Train.Service.Models;

namespace Train.Service.DataReaders
{
    public interface ITrackReader
    {
        Task<List<Track>> Fetch();

        List<string> GetCities();
    }

    public class TrackReader : ITrackReader
    {
        private readonly TrainEntities _db;

        public TrackReader()
        {
            _db = new TrainEntities();
            _db.Database.Connection.Open();
        }

        public Task<List<Track>> Fetch()
        {
            var road1 = _db.Roads.Select(x => x.Distance > 1000).ToList();


            return _db.Roads
                .Include(x => x.FromCity)
                .Include(x => x.ToCity)
                .Select(x => new Track
                {
                    From = x.FromCity.Name,
                    To = x.ToCity.Name,
                    Distance = x.Distance
                })
                .ToListAsync();
        }

        public List<string> GetCities()
        {
            return _db.Cities.Select(x => x.Name).ToList();
        }
    }
}