using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using Train.Service.DataReaders;
using Train.Web.Models;
using System.Linq;

namespace Train.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITrackReader _reader;

        public List<string> Cities { get; set; }

        public HomeController() : this((ITrackReader)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ITrackReader)))
        {

        }

        public HomeController(ITrackReader reader)
        {
            _reader = reader;
        }

        public ActionResult Index()
        {
            var model = new Map
            {
                Cities = (from city in _reader.GetCities()
                          select new SelectListItem
                          {
                              Value = city,
                              Text = city
                          }).ToList()
            };

            model.Cities.Insert(0, new SelectListItem
            {
                Text = "-- City --",
                Value = null
            });

            return View(model);
        }
    }
}
