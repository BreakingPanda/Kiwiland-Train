using Train.Service.Models;

namespace Train.Service.Queue
{
    public interface ITrainQueue
    {
        void Enqueue(Town town, int distance, Route route);

        bool HasData { get; }

        void Dequeue();

        Town CurrentTown { get; }

        Route CurrentRoute { get; }
    }
}