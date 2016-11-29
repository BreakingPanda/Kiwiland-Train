using System.Collections.Generic;
using System.Threading.Tasks;
using Train.Service.Models;

namespace Train.Service.DataReaders
{
    public interface ITrackReader 
    {
        Task<IEnumerable<Track>> ReadFromFile(string filepath);
    }
}