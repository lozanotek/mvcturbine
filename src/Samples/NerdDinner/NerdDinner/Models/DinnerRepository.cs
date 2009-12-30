using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NerdDinner.Models {

    public class DinnerRepository : NerdDinner.Models.IDinnerRepository {

        NerdDinnerDataContext db = new NerdDinnerDataContext();

        //
        // Query Methods

        public IQueryable<Dinner> FindAllDinners() {
            return db.Dinners;
        }

        public IQueryable<Dinner> FindUpcomingDinners() {
            return from dinner in FindAllDinners()
                   where dinner.EventDate >= DateTime.Now
                   orderby dinner.EventDate
                   select dinner;
        }

        public IQueryable<Dinner> FindByLocation(float latitude, float longitude) {
            var dinners = from dinner in FindUpcomingDinners()
                          join i in db.NearestDinners(latitude, longitude) 
                          on dinner.DinnerID equals i.DinnerID
                          select dinner;

            return dinners;
        }

        public Dinner GetDinner(int id) {
            return db.Dinners.SingleOrDefault(d => d.DinnerID == id);
        }

        //
        // Insert/Delete Methods

        public void Add(Dinner dinner) {
            db.Dinners.InsertOnSubmit(dinner);
        }

        public void Delete(Dinner dinner) {
            db.RSVPs.DeleteAllOnSubmit(dinner.RSVPs);
            db.Dinners.DeleteOnSubmit(dinner);
        }

        //
        // Persistence 

        public void Save() {
            db.SubmitChanges();
        }
    }
}
