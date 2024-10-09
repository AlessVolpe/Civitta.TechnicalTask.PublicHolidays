using Civitta.TechnicalTask.PublicHolidays.Models;
using Civitta.TechnicalTask.PublicHolidays.Models.DTOs;
using Civitta.TechnicalTask.PublicHolidays.Models.Requests;
using Civitta.TechnicalTask.PublicHolidays.Models.Responses;
using Civitta.TechnicalTask.PublicHolidays.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;

namespace Civitta.TechnicalTask.PublicHolidays.Services
{
    public class HolidayService(AppDbContext context) : IHolidayService {
        private readonly string _urlBase = "https://kayaposoft.com";
        private readonly AppDbContext _context = context;
        public async Task<IEnumerable<Holiday>> GetHolidaysByMonthAsync(HolidayByMonthReq reqParameters) {
            IList<Holiday> holidays = [];

            if (reqParameters == null) return holidays;
            if (!context.Holidays.Any()) {
                var webData = GetHolidaysByMonthWebAsync(reqParameters).Result;
                foreach (var holidayDTO in webData) {
                    List<int> nameKeys = []; int key = 0;
                    foreach (var value in holidayDTO.Names) {
                        nameKeys.Add(key++);
                        HolidayName name = new() { 
                            Lang = value.Lang,
                            Text = value.Text
                        };
                        _context.HolidayNames.Add(name);
                    }
                    Holiday holiday = new() { 
                        Date = holidayDTO.Date,
                        Names = nameKeys,
                        HolidayType = holidayDTO.HolidayType,
                    };
                    holidays.Add(holiday);
                }
                _context.Holidays.AddRange(holidays);
                return holidays;
            }
            return await _context.Holidays.ToListAsync();
        }

        private async Task<IList<HolidayDTO>> GetHolidaysByMonthWebAsync(HolidayByMonthReq reqParameters) {
            IList<HolidayDTO> holidays = [];
            if (!ReqParamCheck(reqParameters)) return holidays;

            string requestURI = $"/enrico/json/v3.0/getHolidaysForMonthmonth={reqParameters.Month}&year={reqParameters.Year}&country={reqParameters.Country}";
            if (reqParameters.Region != null) requestURI += $"&region={reqParameters.Region}";
            requestURI += $"&holidayType={reqParameters.HolidayType}";

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
                        holiday.Date.Year,
                        holiday.Date.Month,
                        holiday.Date.Day
                    );
                    
                    var zippedLists = holiday.Langs.Zip(holiday.Texts, (l, t) => new { Lang = l, Text = t });
                    foreach (var lt in zippedLists) {
                        HolidayNameDTO name = new() { 
                            Lang = lt.Lang, 
                            Text = lt.Text
                        };
                        names.Add(name);
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

        private bool ReqParamCheck(HolidayByMonthReq reqParameters) => (
            Enumerable.Range(1, 12).Contains(reqParameters.Month) &&
            reqParameters.Year < _context.Countries.Where(country => country.CountryCode.Equals(reqParameters.Country)).FirstOrDefaultAsync().Result.ToDate.Date.Year &&
            _context.Countries.Where(country => country.CountryCode.Equals(reqParameters.Country)).Any() &&
            _context.Countries.Where(country => country.CountryCode.Equals(reqParameters.Country)).FirstOrDefaultAsync().Result.Regions.Contains(reqParameters.Region));
    }
}
