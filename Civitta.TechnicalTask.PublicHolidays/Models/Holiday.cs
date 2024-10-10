using Microsoft.AspNetCore.Routing.Constraints;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Civitta.TechnicalTask.PublicHolidays.Models {
    public class Holiday {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HolidayId { get; set; }
        public DateTime Date { get; set; }
        public int DayOfWeek { get; set; }
        [ForeignKey("NameId")]
        public IList<HolidayName> Names { get; set; }
        public string HolidayType { get; set; }
    }
}
