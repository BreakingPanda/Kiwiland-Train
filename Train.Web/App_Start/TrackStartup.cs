using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Train.Service.DataReaders;
using Train.Service.Models;

namespace Train.Web
{
    public class TrackStartup
    {
        public static void Start()
        {
            ITrackReader reader = (ITrackReader) GlobalConfiguration.Configuration
                                                                    .DependencyResolver
                                                                    .GetService(typeof(ITrackReader));                      

            IEnumerable<Track> tracks = reader.Fetch().Result;

            HttpRuntime.Cache.Insert("tracks", tracks);
        }
    }
}