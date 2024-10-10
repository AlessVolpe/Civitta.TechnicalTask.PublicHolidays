using Newtonsoft.Json;

namespace Civitta.TechnicalTask.PublicHolidays.Models.Responses
{
    public class HolidayResponse {
        public DateResponse Date { get; set; }
        [JsonProperty("name")]
        public IList<HolidayNameResponse> Names { get; set; }
        public string HolidayType { get; set; }
    }


}
