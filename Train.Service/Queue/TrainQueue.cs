using System.Collections.Generic;
using System.Linq;
using Train.Service.Models;

namespace Train.Service.Queue
{
    public class TrainQueue : ITrainQueue
    {
        private readonly Queue<KeyValuePair<Town, Route>> _queue;

        public TrainQueue()
        {
            _queue = new Queue<KeyValuePair<Town, Route>>();
        }

        public void Enqueue(Town town, int distance, Route route)
        {
            _queue.Enqueue(new KeyValuePair<Town, Route>(town, new Route
            {
                Path = route.Path + town.Name + "-",
                Distance = route.Distance + distance
            }));
        }

        public bool HasData => _queue.Any();

        public void Dequeue()
        {
            var node = _queue.Dequeue();

            CurrentTown = node.Key;
            CurrentRoute = node.Value;
        }

        public Town CurrentTown { get; private set; }

        public Route CurrentRoute { get; private set; }
    }
}
