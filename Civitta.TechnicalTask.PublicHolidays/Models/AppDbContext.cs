using Microsoft.EntityFrameworkCore;

namespace Civitta.TechnicalTask.PublicHolidays.Models {
    public partial class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Holiday> Holidays { get; set; } = null!;
        public virtual DbSet<HolidayName> HolidayNames { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Country>(entity => {
                entity.HasKey(country => country.CountryCode);
            });
            modelBuilder.Entity<Holiday>(entity => {
                entity
                .HasKey(holiday => holiday.HolidayId);
            });
            modelBuilder.Entity<HolidayName>(entity => {
                entity.HasKey(holidayName => new { holidayName.Lang, holidayName.Text });
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
