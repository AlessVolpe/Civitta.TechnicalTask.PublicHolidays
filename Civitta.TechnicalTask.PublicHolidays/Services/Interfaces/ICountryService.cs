
using Civitta.TechnicalTask.PublicHolidays.Models;
using Microsoft.AspNetCore.Mvc;

namespace Civitta.TechnicalTask.PublicHolidays.Services.Interfaces {
    public interface ICountryService {
        Task<IEnumerable<Country>?> GetAllAsync();
    }
}
