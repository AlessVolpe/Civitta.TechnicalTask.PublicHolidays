namespace Civitta.TechnicalTask.PublicHolidays.Models.Responses
{
    public class CountryResponse
    {
        public string CountryCode { get; set; }
        public List<string> Regions { get; set; }
        public List<string> HolidayTypes { get; set; }
        public string FullName { get; set; }
        public Date FromDate { get; set; }
        public Date ToDate { get; set; }
    }

    public class Date
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
