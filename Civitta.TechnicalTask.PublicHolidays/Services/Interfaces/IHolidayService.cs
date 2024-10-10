using Civitta.TechnicalTask.PublicHolidays.Models;
using Civitta.TechnicalTask.PublicHolidays.Models.Responses;

namespace Civitta.TechnicalTask.PublicHolidays.Services.Interfaces {
    public interface IHolidayService {
        Task<IEnumerable<Holiday>> GetHolidaysByMonthAsync(int month, int year, string country, string? region, string holidayType);
        Task<GetMaximumFreeDaysResponse> GetMaximumFreeDaysAsync(int year, string country, string? region);
        Task<IsPublicHolidayResponse> IsPublicHolidayAsync(string date, string country, string? region);
    }
}
