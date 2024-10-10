using Civitta.TechnicalTask.PublicHolidays.Controllers;
using Civitta.TechnicalTask.PublicHolidays.Models;
using Civitta.TechnicalTask.PublicHolidays.Models.Responses;
using Civitta.TechnicalTask.PublicHolidays.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Civitta.TechnicalTask.PublicHolidays.Tests {
    public class HolidayService_Tests(HolidayController controller, IHolidayService service) {
        private readonly HolidayController _controller = controller;
        private readonly IHolidayService _service = service;

        [Fact]
        public void GetHolidayByMonth_Success() {
            // Arrange
            int valid_month = 1,
                valid_year = 2024;

            string? valid_country = "ita",
                valid_region = null,
                valid_holidayType = "all";


            // Act
            var successResult = _controller.GetHolidaysByMonth(valid_month, valid_year, valid_country, valid_region, valid_holidayType);
            var successModel = successResult as OkObjectResult;
            var resultList = successModel.Value as List<Country>;

            // Assert
            Assert.NotNull(successResult);
            Assert.IsType<List<Country>>(successModel.Value);
            Assert.Equal(_controller.GetHolidaysByMonth(valid_month, valid_year, valid_country, valid_region, valid_holidayType).Count(), resultList.Count);
        }

        [Fact]
        public void IsPublicHoliday_Success() {
            // Arrange

            string? valid_date = "2024-01-01",
                valid_country = "ita",
                valid_region = null;


            // Act
            var successResult = _controller.IsPublicHoliday(valid_date, valid_country, valid_region);
            var successModel = successResult as IsPublicHolidayResponse;


            // Assert
            Assert.NotNull(successResult);
            Assert.IsType<bool>(successModel.IsPublicHoliday);
            Assert.Equal(successModel, _controller.IsPublicHoliday(valid_date, valid_country, valid_region));
        }

        [Fact]
        public void GetMaximumFreeDays_Success() {
            // Arrange
            int valid_year = 2024;

            string? valid_country = "ita",
                valid_region = null;

            // Act
            var successResult = _controller.GetMaximumFreeDays(valid_year, valid_country, valid_region);
            var successModel = successResult as GetMaximumFreeDaysResponse;


            // Assert
            Assert.NotNull(successResult);
            Assert.IsType<int>(successModel.MaxDays);
            Assert.Equal(successModel, _controller.GetMaximumFreeDays(valid_year, valid_country, valid_region));
        }
    }
}
