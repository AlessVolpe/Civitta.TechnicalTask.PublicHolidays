using Civitta.TechnicalTask.PublicHolidays.Models;
using Civitta.TechnicalTask.PublicHolidays.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Civitta.TechnicalTask.PublicHolidays.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class CountryController(ICountryService service) : ControllerBase {
        private readonly ICountryService _service = service;

        /// <summary>Returns the list of supported countries</summary>
        /// <returns>Returns the list of supported countries</returns>
        [HttpGet]
        public IEnumerable<Country> GetCountryList() => _service.GetAllAsync().Result;
    }
}
