using Civitta.TechnicalTask.PublicHolidays.Models;
using Civitta.TechnicalTask.PublicHolidays.Models.DTOs;
using Civitta.TechnicalTask.PublicHolidays.Models.Responses;
using Civitta.TechnicalTask.PublicHolidays.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;

namespace Civitta.TechnicalTask.PublicHolidays.Services {
    public class NotImplementedException(AppDbContext context) : IHolidayService {
        private readonly string _urlBase = "https://kayaposoft.com";
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Holiday>> GetHolidaysByMonthAsync(int month, int year, string country, string? region, string holidayType) {
            IList<Holiday> holidays = [];

            if (!context.Holidays.Any()) {
                var webData = GetHolidaysByMonthWebAsync(month, year, country, region, holidayType).Result;
                if (webData == null) return holidays;
                foreach (var holidayDTO in webData) {
                    IList<HolidayName> names = [];
                    foreach (var value in holidayDTO.Names) {
                        HolidayName name = new() {
                            Lang = value.Lang,
                            Text = value.Text
                        };
                        names.Add(name);
                        _context.HolidayNames.Add(name);
                    }
                    Holiday holiday = new() {
                        Date = holidayDTO.Date,
                        Names = names,
                        HolidayType = holidayDTO.HolidayType,
                    };
                    holidays.Add(holiday);
                }
                _context.Holidays.AddRange(holidays);
                return holidays;
            }
            return await _context.Holidays.ToListAsync();
        }

        private async Task<IList<HolidayDTO>> GetHolidaysByMonthWebAsync(int month, int year, string country, string? region, string holidayType) {
            IList<HolidayDTO> holidays = [];

            string requestURI = $"/enrico/json/v3.0/getHolidaysForMonth?month={month}&year={year}&country={country}";
            if (region != null) requestURI += $"&region={region}";
            requestURI += $"&holidayType={holidayType}";

            var client = new RestClient(_urlBase);
            var request = new RestRequest(requestURI, Method.Get);
            request.AddHeader("Access-Control-Allow-Origin", "*");
            request.AddHeader("Content-Type", "application/json");
            RestResponse response = await client.ExecuteAsync(request);


            var data = JsonConvert.DeserializeObject<List<HolidayResponse>>(response.Content);
            if (data != null) {
                foreach (HolidayResponse holiday in data) {
                    List<HolidayNameDTO> names = [];
                    DateTime date = new(
                        holiday.Date.year,
                        holiday.Date.month,
                        holiday.Date.day
                    );

                    foreach (var name in holiday.Names) {
                        HolidayNameDTO nameDTO = new() {
                            Lang = name.Lang,
                            Text = name.Text
                        };
                        names.Add(nameDTO);
                    }

                    HolidayDTO holidayDTO = new() {
                        Date = date,
                        Weekday = holiday.Date.DayOfWeek,
                        Names = names,
                        HolidayType = holiday.HolidayType
                    };
                    holidays.Add(holidayDTO);
                }
            }
            return holidays;
        }
    }
}
