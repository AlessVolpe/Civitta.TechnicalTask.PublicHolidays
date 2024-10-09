using Civitta.TechnicalTask.PublicHolidays.Models;
using Civitta.TechnicalTask.PublicHolidays.Models.DTOs;
using Civitta.TechnicalTask.PublicHolidays.Models.Responses;
using Civitta.TechnicalTask.PublicHolidays.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics.Metrics;

namespace Civitta.TechnicalTask.PublicHolidays.Services
{
    public class CountryService(AppDbContext context) : ICountryService {
        private readonly string _urlBase = "https://kayaposoft.com";
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Country>?> GetAllAsync() {
            IList<Country> countries = [];

            if (!_context.Countries.Any()) {
                var webData = GetAllWebAsync().Result;
                if (!webData.Any()) return null;
                foreach (var countryDTO in webData) {
                    Country country = new() {
                        CountryCode = countryDTO.CountryCode,
                        Regions = countryDTO.Regions,
                        HolidayTypes = countryDTO.HolidayTypes,
                        FullName = countryDTO.FullName,
                        FromDate = countryDTO.FromDate,
                        ToDate = countryDTO.ToDate
                    };
                    countries.Add(country);
                }
                _context.Countries.AddRange(countries);
                return countries;
            }
            return await _context.Countries.ToListAsync();
        }   

        private async Task<IList<CountryDTO>> GetAllWebAsync() {
            IList<CountryDTO> countries = [];

            var client = new RestClient(_urlBase);
            var request = new RestRequest("/enrico/json/v3.0/getSupportedCountries", Method.Get);
            request.AddHeader("Access-Control-Allow-Origin", "*");
            request.AddHeader("Content-Type", "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            var data = JsonConvert.DeserializeObject<IList<CountryResponse>>(response.Content);
            if (data != null) {
                foreach (CountryResponse country in data) {
                    DateTime dateFrom = new(
                        country.FromDate.Year,
                        country.FromDate.Month,
                        country.FromDate.Day
                    );
                    DateTime dateTo = new(
                        country.ToDate.Year > 9999 ? 9999 : country.ToDate.Year,
                        country.ToDate.Month,
                        country.ToDate.Day
                    );

                    CountryDTO countryDTO = new() {
                        CountryCode = country.CountryCode,
                        Regions = country.Regions,
                        HolidayTypes = country.HolidayTypes,
                        FullName = country.FullName,
                        FromDate = dateFrom,
                        ToDate = dateTo
                    };
                    countries.Add(countryDTO);
                }
            }
            return countries;
        }
    }
}
