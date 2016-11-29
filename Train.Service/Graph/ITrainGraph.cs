using System.Collections.Generic;
using Train.Service.Models;

namespace Train.Service.Graph
{
    public interface ITrainGraph
    {
        void AddRoute(Track track);

        IEnumerable<Route> FindRoutes(Request request);

        IEnumerable<Route> FindRoutes(string from, string to);

        int? FindDistance(string town, params string[] stops);

        Route FindShortest(string from, string to);
    }
}
