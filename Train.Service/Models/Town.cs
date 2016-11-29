using System.Collections.Generic;

namespace Train.Service.Models
{
    public class Town
    {
        public string Name { get; set; }

        public IDictionary<Town, int> Neighbors { get; set; }

        public Town()
        {
            Neighbors = new Dictionary<Town, int>();
        }

        public void AddNeighbor(Town neighbor, int distance)
        {
            Neighbors.Add(neighbor, distance);
        }
    }
}
