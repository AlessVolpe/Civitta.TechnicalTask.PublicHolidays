using Civitta.TechnicalTask.PublicHolidays.Models;
using Civitta.TechnicalTask.PublicHolidays.Models.Requests;
using Civitta.TechnicalTask.PublicHolidays.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Civitta.TechnicalTask.PublicHolidays.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class HolidayController(IHolidayService service, ILogger<HolidayController> logger) : ControllerBase {
        private readonly IHolidayService _service = service;
        private readonly ILogger<HolidayController> _logger = logger;

        [HttpPost("query")]
        public IEnumerable<Holiday> GetHolidaysByMonth(HolidayByMonthReq reqParameters) => _service.GetHolidaysByMonthAsync(reqParameters).Result;
    }
}
