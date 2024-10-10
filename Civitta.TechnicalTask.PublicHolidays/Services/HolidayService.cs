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

        public async Task<IsPublicHolidayResponse> IsPublicHolidayAsync(string date, string country, string? region) {
            if (!context.Holidays.Any()) return await IsPublicHolidayWebAsync(date, country, region);

            var holidays = await GetHolidaysByMonthAsync(Int32.Parse(date.Split("-")[1]), Int32.Parse(date.Split("-")[0]), country, region, "all");
            if (holidays.Where(h => h.Date == DateTime.Parse(date)).Any()) return new IsPublicHolidayResponse { IsPublicHoliday = true };
            return new IsPublicHolidayResponse { IsPublicHoliday = false };
        }

        private async Task<IsPublicHolidayResponse> IsPublicHolidayWebAsync(string date, string country, string region) {
            string requestURI = $"/enrico/json/v3.0/isPublicHoliday?date={date}&country={country}";
            if (region != null) requestURI += $"&region={region}";

            var client = new RestClient(_urlBase);
            var request = new RestRequest(requestURI, Method.Get);
            request.AddHeader("Access-Control-Allow-Origin", "*");
            request.AddHeader("Content-Type", "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            var data = JsonConvert.DeserializeObject<IsPublicHolidayResponse>(response.Content);
            IsPublicHolidayResponse isPublicHolidayResponse = new() { IsPublicHoliday = data.IsPublicHoliday };
            return isPublicHolidayResponse;
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


