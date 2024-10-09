namespace Civitta.TechnicalTask.PublicHolidays.Models.DTOs {
    public class CountryDTO {
        public string CountryCode { get; set; }
        public List<string> Regions { get; set; }
        public List<string> HolidayTypes { get; set; }
        public string FullName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
