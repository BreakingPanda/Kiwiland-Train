namespace Train.Service.Models
{
    public class Request
    {

        public string From { get; set; }

        public string To { get; set; }

        public int? MaxStop { get; set; } 

        public int? DistanceLessThan { get; set; } 

        public bool AllowCircularRoute { get; set; }

        public static Request BuildSimple(string @from, string to)
        {
            return new Request
            {
                From = from,
                To = to
            };
        }
    }
}
