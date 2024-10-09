using Microsoft.AspNetCore.Mvc;

namespace Civitta.TechnicalTask.PublicHolidays.Models.Requests {
    [BindProperties]
    public class HolidayByMonthReq {
        public required int Month { get; set; }
        public required int Year { get; set; }
        public required string Country { get; set; }
        public string? Region { get; set; } = null;
        public string? HolidayType { get; set; } = "all";
    }
}
