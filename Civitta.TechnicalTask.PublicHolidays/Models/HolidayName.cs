using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Civitta.TechnicalTask.PublicHolidays.Models {
    public class HolidayName {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NameId { get; set; }
        public string Lang { get; set; }
        public string Text { get; set; }
    }
}