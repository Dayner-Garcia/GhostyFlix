using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Gender> Genders { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Series> Series { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Tables

            modelBuilder.Entity<Gender>().ToTable("Genders");
            modelBuilder.Entity<Producer>().ToTable("Producers");
            modelBuilder.Entity<Series>().ToTable("Series");

            #endregion

            #region PrimaryKeys

            modelBuilder.Entity<Gender>().HasKey(gender => gender.Id);
            modelBuilder.Entity<Gender>().Property(gender => gender.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Producer>().HasKey(producer => producer.Id);
            modelBuilder.Entity<Producer>().Property(producer => producer.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Series>().HasKey(series => series.Id);
            modelBuilder.Entity<Series>().Property(series => series.Id).ValueGeneratedOnAdd();

            #endregion

            #region RelationShips

            modelBuilder.Entity<Series>()
                .HasOne(series => series.Producer)
                .WithMany(producer => producer.Series)
                .HasForeignKey(series => series.ProducerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration Series and the Primary Genre
            modelBuilder.Entity<Series>()
                .HasOne(series => series.GenderPrimary)
                .WithMany(gender => gender.SeriesAsPrimary)
                .HasForeignKey(series => series.GenderPrimaryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration Series and the Secondary Genre
            modelBuilder.Entity<Series>()
                .HasOne(series => series.GenderSecondary)
                .WithMany(gender => gender.SeriesAsSecondary)
                .HasForeignKey(series => series.GenderSecondaryId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region "Property Configurations"

            #region Series

            modelBuilder.Entity<Series>()
                .Property(x => x.CoverImage)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<Series>()
                .Property(x => x.LinkVideo)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<Series>()
                .Property(series => series.Name)
                .IsRequired()
                .HasMaxLength(100);

            #endregion

            #region Producers

            modelBuilder.Entity<Producer>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            #endregion

            #region Genders

            modelBuilder.Entity<Gender>()
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(50);

            #endregion

            #endregion
        }
    }
}