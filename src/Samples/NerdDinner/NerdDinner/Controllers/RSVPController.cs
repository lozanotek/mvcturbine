using System.Web.Mvc;
using NerdDinner.Models;

namespace NerdDinner.Controllers {
    [HandleErrorWithELMAH]
    public class RSVPController : Controller {
        private readonly IDinnerRepository dinnerRepository;

        public RSVPController(IDinnerRepository repository) {
            dinnerRepository = repository;
        }

        //
        // AJAX: /Dinners/Register/1

        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(int id) {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (!dinner.IsUserRegistered(User.Identity.Name)) {
                var rsvp = new RSVP();
                rsvp.AttendeeName = User.Identity.Name;

                dinner.RSVPs.Add(rsvp);
                dinnerRepository.Save();
            }

            return Content("Thanks - we'll see you there!");
        }
    }
}