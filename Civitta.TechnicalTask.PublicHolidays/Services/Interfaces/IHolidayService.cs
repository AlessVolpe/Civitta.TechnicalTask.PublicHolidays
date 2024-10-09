using Civitta.TechnicalTask.PublicHolidays.Models;
using Civitta.TechnicalTask.PublicHolidays.Models.Requests;

namespace Civitta.TechnicalTask.PublicHolidays.Services.Interfaces {
    public interface IHolidayService {
        Task<IEnumerable<Holiday>> GetHolidaysByMonthAsync(HolidayByMonthReq reqParameters);
    }
}
