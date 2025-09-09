using System.Text.RegularExpressions;
using BACKEND.Models;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Data
{
    /// <summary>
    /// DbContext klasa koja predstavlja bazu podataka Edunova.
    /// Sadrži DbSet-ove za sve entitete i konfigurira relacije između njih.
    /// </summary>
    public class EdunovaContext : DbContext
    {
        public EdunovaContext(DbContextOptions<EdunovaContext> options) : base(options)
        {
        }

        public DbSet<Lokacija> Lokacije { get; set; }
        public DbSet<Analiticar> Analiticari { get; set; }
        public DbSet<Uzorcitla> UzorciTla { get; set; }
        public DbSet<Analiza> Analize { get; set; }
        public DbSet<Operater> Operateri { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Operateri
            modelBuilder.Entity<Operater>().ToTable("operateri");

            // Lokacije
            modelBuilder.Entity<Lokacija>().ToTable("lokacije");

            // Analiticari
            modelBuilder.Entity<Analiticar>().ToTable("analiticari");
            modelBuilder.Entity<Analiticar>()
                .Property(a => a.SlikaUrl)
                .HasColumnName("slikaurl");

            // UzorciTla
            modelBuilder.Entity<Uzorcitla>().ToTable("uzorcitla");
            modelBuilder.Entity<Uzorcitla>()
                .HasOne(u => u.Lokacija)
                .WithMany(l => l.UzorciTla)
                .HasForeignKey(u => u.LokacijaId)
                .HasConstraintName("fk_uzorcitla_lokacije");

            // Analize
            modelBuilder.Entity<Analiza>().ToTable("analize");

            modelBuilder.Entity<Analiza>()
                .Property(a => a.AnaliticarId)
                .HasColumnName("analiticar");

            modelBuilder.Entity<Analiza>()
                .Property(a => a.UzorakTlaId)
                .HasColumnName("uzoraktla");

            modelBuilder.Entity<Analiza>()
                .HasOne(a => a.Analiticar)
                .WithMany(an => an.Analize)
                .HasForeignKey(a => a.AnaliticarId)
                .HasConstraintName("fk_analize_analiticari");

            modelBuilder.Entity<Analiza>()
                .HasOne(a => a.UzorakTla)
                .WithMany(u => u.Analize)
                .HasForeignKey(a => a.UzorakTlaId)
                .HasConstraintName("fk_analize_uzorcitla");
        }
    }
}
