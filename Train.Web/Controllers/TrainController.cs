using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Train.Service.Graph;
using Train.Service.Models;

namespace Train.Web.Controllers
{
    [RoutePrefix("api/train")]
    public class TrainController : ApiController
    {
        private readonly ITrainGraph _service;

        public TrainController(ITrainGraph trainGraphService)
        {
            _service = trainGraphService;

            ImportTracks();
        }

        private void ImportTracks()
        {
            var tracks = (IEnumerable<Track>) HttpRuntime.Cache["tracks"];

            foreach (Track track in tracks)
            {
                _service.AddRoute(track);
            }
        }

        [HttpGet]
        [Route("{source}/trip/{destination}")]
        public HttpResponseMessage Get(string source, string destination)
        {
            var routes = _service.FindRoutes(source, destination).ToList();

            if (!routes.Any())
            {
                return Request.CreateResponse("NO SUCH ROUTE");
            }

            // sort by shortest to longest
            routes = routes.OrderBy(r => r.Distance).ToList();

            var message = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(System.Web.Helpers.Json.Encode(routes))
            };

            return message;
        }
    }
}
