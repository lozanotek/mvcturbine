using System;
using System.Globalization;
using System.Linq;
using System.Web.Caching;
using System.Web.Mvc;
using System.Xml.Linq;
using NerdDinner.Helpers;
using NerdDinner.Models;

namespace NerdDinner.Controllers {
    public class JsonDinner {
        public int DinnerID { get; set; }
        public DateTime EventDate { get; set; }
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
        public int RSVPCount { get; set; }
        public string Url { get; set; }
    }

    [HandleErrorWithELMAH]
    public class SearchController : Controller {
        private readonly IDinnerRepository dinnerRepository;

        public SearchController(IDinnerRepository repository) {
            dinnerRepository = repository;
        }

        //
        // AJAX: /Search/FindByLocation?longitude=45&latitude=-90

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SearchByLocation(float latitude, float longitude) {
            IQueryable<Dinner> dinners = dinnerRepository.FindByLocation(latitude, longitude);

            IQueryable<JsonDinner> jsonDinners = from dinner in dinners
                                                 select JsonDinnerFromDinner(dinner);

            return Json(jsonDinners.ToList());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SearchByPlaceNameOrZip(string placeOrZip) {
            if (String.IsNullOrEmpty(placeOrZip)) return null;
            ;

            string url = "http://ws.geonames.org/postalCodeSearch?{0}={1}&maxRows=1&style=SHORT";
            url = String.Format(url, IsNumeric(placeOrZip) ? "postalcode" : "placename", placeOrZip);

            var result = ControllerContext.HttpContext.Cache[placeOrZip] as XDocument;
            if (result == null) {
                result = XDocument.Load(url);
                ControllerContext.HttpContext.Cache.Insert(placeOrZip, result, null, DateTime.Now.AddDays(1),
                                                           Cache.NoSlidingExpiration);
            }

            var LatLong = (from x in result.Descendants("code")
                           select new
                                      {
                                          Lat = (float)x.Element("lat"),
                                          Long = (float)x.Element("lng")
                                      }).First();

            IOrderedQueryable<Dinner> dinners = dinnerRepository.
                FindByLocation(LatLong.Lat, LatLong.Long).
                OrderByDescending(p => p.EventDate);

            return View("Results", new PaginatedList<Dinner>(dinners, 0, 20));
        }

        // IsNumeric Function
        private bool IsNumeric(object Expression) {
            // Variable to collect the Return value of the TryParse method.
            bool isNum;

            // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
            double retNum;

            // The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.
            // The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.
            isNum = Double.TryParse(Convert.ToString(Expression), NumberStyles.Any, NumberFormatInfo.InvariantInfo,
                                    out retNum);
            return isNum;
        }

        //
        // AJAX: /Search/GetMostPopularDinners
        // AJAX: /Search/GetMostPopularDinners?limit=5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetMostPopularDinners(int? limit) {
            IQueryable<Dinner> dinners = dinnerRepository.FindUpcomingDinners();

            // Default the limit to 40, if not supplied.
            if (!limit.HasValue)
                limit = 40;

            IQueryable<JsonDinner> mostPopularDinners = from dinner in dinners
                                                        orderby dinner.RSVPs.Count descending
                                                        select JsonDinnerFromDinner(dinner);

            return Json(mostPopularDinners.Take(limit.Value).ToList());
        }

        private JsonDinner JsonDinnerFromDinner(Dinner dinner) {
            return new JsonDinner
                       {
                           DinnerID = dinner.DinnerID,
                           EventDate = dinner.EventDate,
                           Latitude = dinner.Latitude,
                           Longitude = dinner.Longitude,
                           Title = dinner.Title,
                           Description = dinner.Description,
                           RSVPCount = dinner.RSVPs.Count,
                           //TODO: Need to mock this out for testing...
                           //Url = Url.RouteUrl("PrettyDetails", new { Id = dinner.DinnerID } )
                           Url = dinner.DinnerID.ToString()
                       };
        }
    }
}
