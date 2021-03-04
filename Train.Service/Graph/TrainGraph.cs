using System.Collections.Generic;
using System.Linq;
using Train.Service.Models;
using Train.Service.Queue;

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

    public class TrainGraph : ITrainGraph
    {
        private readonly ITrainQueue _queue;
        private readonly IList<Town> _towns = new List<Town>();

        public TrainGraph(ITrainQueue queue)
        {
            _queue = queue;
        }

        private Town AddOrGetTown(string name)
        {
            var town = _towns.FirstOrDefault(a => a.Name == name);
            if (town == null)
            {
                town = new Town { Name = name };
                _towns.Add(town);
            }
            return town;
        }

        public void AddRoute(Track track)
        {
            Town fromTown = AddOrGetTown(track.From);
            Town toTown = AddOrGetTown(track.To);

            fromTown.AddNeighbor(toTown, track.Distance);
        }

        public int? FindDistance(string town, params string[] stops)
        {
            int? totalDistance = 0;
            string from = town;
            foreach (string stop in stops)
            {
                string to = stop;

                int? distance = FindDistance(from, to);
                if (!distance.HasValue)
                    return null;

                totalDistance += distance;
                from = to;
            }
            return totalDistance;
        }

        private int? FindDistance(string from, string to)
        {
            Town fromTown = _towns.First(a => a.Name == from);

            foreach(KeyValuePair<Town,int> neighbor in fromTown.Neighbors)
            {
                if (neighbor.Key.Name == to)
                    return neighbor.Value;
            }
            return null;
        }

        public IEnumerable<Route> FindRoutes(string from, string to)
        {
            return FindRoutes(Request.BuildSimple(from, to));
        }

        public IEnumerable<Route> FindRoutes(Request request)
        {
            var routes = Compute(request).ToList();

            return routes.Distinct();
        }

        private IEnumerable<Route> Compute(Request request)
        {
            var town = _towns.FirstOrDefault(a => a.Name == request.From);

            _queue.Enqueue(town, 0, Route.Empty);

            while (_queue.HasData)
            {
                _queue.Dequeue();

                foreach (var neighbor in _queue.CurrentTown.Neighbors)
                {
                    town = neighbor.Key;
                    int distance = neighbor.Value;

                    if ((_queue.CurrentRoute.Distance + distance) >= (request.DistanceLessThan ?? int.MaxValue))
                    {
                        continue;
                    }

                    if (_queue.CurrentRoute.Stops > (request.MaxStop ?? int.MaxValue))
                    {
                        continue;
                    }

                    if (town.Name == request.To)
                    {
                        if (request.AllowCircularRoute)
                        {
                            _queue.Enqueue(town, distance, _queue.CurrentRoute);
                        }

                        yield return _queue.CurrentRoute.NewRouteTo(request.To, distance);
                    }
                    else
                    {
                        if (_queue.CurrentRoute.TownHasVisited(town) && !request.AllowCircularRoute)
                        {
                            continue;
                        }

                        _queue.Enqueue(town, distance, _queue.CurrentRoute);
                    }
                }
            }
        }

        public Route FindShortest(string from, string to)
        {
            IList<Route> routes = FindRoutes(Request.BuildSimple(from, to)).ToList();

            int minDistance = routes.Min(a => a.Distance);

            return routes.FirstOrDefault(a => a.Distance == minDistance);
        }
    }
}
