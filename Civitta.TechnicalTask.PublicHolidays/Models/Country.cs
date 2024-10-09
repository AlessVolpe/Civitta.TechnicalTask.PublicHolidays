using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Civitta.TechnicalTask.PublicHolidays.Models {
    public class Country {
        [Key]
        public string CountryCode { get; set; }
        public IList<string> Regions { get; set; }
        public IList<string> HolidayTypes { get; set; }
        public string FullName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
