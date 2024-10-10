using Civitta.TechnicalTask.PublicHolidays.Controllers;
using Civitta.TechnicalTask.PublicHolidays.Models;
using Civitta.TechnicalTask.PublicHolidays.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;



namespace Civitta.TechnicalTask.PublicHolidays.Tests {
    public class CountryService_Tests(CountryController controller, ICountryService service) {
        private readonly CountryController _controller = controller;
        private readonly ICountryService _service = service;

        [Fact]
        public void GetAllCountries_Success() {
            // Arrange

            // Act
            var result = _controller.GetCountryList();
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as List<Country>;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Country>>(resultType.Value);
            Assert.Equal(_controller.GetCountryList().Count(), resultList.Count);
        }
    }
}
