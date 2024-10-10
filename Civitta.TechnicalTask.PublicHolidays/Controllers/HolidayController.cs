using Civitta.TechnicalTask.PublicHolidays.Models;
using Civitta.TechnicalTask.PublicHolidays.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

// [, ]

namespace Civitta.TechnicalTask.PublicHolidays.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class HolidayController(IHolidayService service, ILogger<HolidayController> logger) : ControllerBase {
        private readonly IHolidayService _service = service;
        private readonly ILogger<HolidayController> _logger = logger;

        /// <summary>Returns public holidays during a certain month in a certain country</summary>
        /// <param name="month">Month to return holidays for (1 - January, 2 - February, ..., 12 - December)</param>
        /// <param name="year">Year to return holidays for</param>
        /// <param name="country">ISO 3166-1 alpha-3 country code or ISO 3166-1 alpha-2 country code</param>
        /// <param name="region">Region in the country to return holidays for. Not all countries have region defined</param>
        /// <param name="holidayType">Type of holidays to be returned. Allowed values: all, public_holiday, observance, school_holiday, other_day, entra_working_day</param>
        /// <returns>Holidays for given month in given country</returns>
        [HttpGet("query")]
        public IEnumerable<Holiday> GetHolidaysByMonth([BindRequired] int month, [BindRequired] int year, [BindRequired] string country,
            string? region = null, [AllowedValues(["all", "public_holiday", "observance", "school_holiday", "other_day", "entra_working_day"])]string holidayType = "all") =>
            _service.GetHolidaysByMonthAsync(month, year, country, region, holidayType).Result;
    }
}
