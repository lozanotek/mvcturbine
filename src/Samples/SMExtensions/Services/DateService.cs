namespace Services {
    using System;

    public class DateService : IDateService {
        public DateTime GetCurrentDate() {
            return DateTime.Now;   
        }
    }
}
