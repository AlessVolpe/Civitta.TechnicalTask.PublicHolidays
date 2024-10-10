using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Civitta.TechnicalTask.PublicHolidays.Models.DTOs {
    public class HolidayDTO {
        public DateTime Date { get; set; }
        public int Weekday { get; set; }
        [ForeignKey("NameId")]
        public IList<HolidayNameDTO> Names { get; set; }
        public string HolidayType { get; set; }
    }
}
