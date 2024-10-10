using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Civitta.TechnicalTask.PublicHolidays.Models {
    [PrimaryKey(nameof(Lang), nameof(Text))]
    public class HolidayName {
        public string Lang { get; set; }
        public string Text { get; set; }
    }
}