﻿// <auto-generated />
using System;
using Civitta.TechnicalTask.PublicHolidays.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Civitta.TechnicalTask.PublicHolidays.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241010130903_20241010_1508_migrate")]
    partial class _20241010_1508_migrate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Civitta.TechnicalTask.PublicHolidays.Models.Country", b =>
                {
                    b.Property<string>("CountryCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HolidayTypes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Regions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CountryCode");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Civitta.TechnicalTask.PublicHolidays.Models.Holiday", b =>
                {
                    b.Property<int>("HolidayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HolidayId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<string>("HolidayType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HolidayId");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("Civitta.TechnicalTask.PublicHolidays.Models.HolidayName", b =>
                {
                    b.Property<string>("Lang")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("NameId")
                        .HasColumnType("int");

                    b.HasKey("Lang", "Text");

                    b.HasIndex("NameId");

                    b.ToTable("HolidayNames");
                });

            modelBuilder.Entity("Civitta.TechnicalTask.PublicHolidays.Models.HolidayName", b =>
                {
                    b.HasOne("Civitta.TechnicalTask.PublicHolidays.Models.Holiday", null)
                        .WithMany("Names")
                        .HasForeignKey("NameId");
                });

            modelBuilder.Entity("Civitta.TechnicalTask.PublicHolidays.Models.Holiday", b =>
                {
                    b.Navigation("Names");
                });
#pragma warning restore 612, 618
        }
    }
}
