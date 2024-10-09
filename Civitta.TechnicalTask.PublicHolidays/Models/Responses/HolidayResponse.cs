namespace Civitta.TechnicalTask.PublicHolidays.Models.Responses {
    public class HolidayResponse {
        public DateTime Date { get; set; }
        public IList<string> Langs { get; set; }
        public IList<string> Texts { get; set; } 
        public string HolidayType { get; set; }
        
    }
}
