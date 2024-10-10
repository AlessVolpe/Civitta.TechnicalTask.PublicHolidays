using Civitta.TechnicalTask.PublicHolidays.Models;

namespace Civitta.TechnicalTask.PublicHolidays.Services.Interfaces {
    public interface IHolidayService {
        Task<IEnumerable<Holiday>> GetHolidaysByMonthAsync(int month, int year, string country, string? region, string holidayType);
    }
}
