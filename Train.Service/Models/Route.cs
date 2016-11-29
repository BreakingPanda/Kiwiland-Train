using System.Linq;

namespace Train.Service.Models
{
    public class Route
    {
        public string Path { get; set; }

        public int Distance { get; set; }

        public int Stops => Path.Count(a => a == '-');

        public bool TownHasVisited(Town town)
        {
            return Path.Contains(town.Name);
        }

        public static Route NewRoute(string start)
        {
            return new Route
            {
                Path = $"{start}-"
            };
        }

        public static Route Empty => new Route();


        public Route NewRouteTo(string town, int distance)
        {
            return new Route
            {
                Distance = Distance + distance,
                Path = Path + town
            };
        }
    }
}
