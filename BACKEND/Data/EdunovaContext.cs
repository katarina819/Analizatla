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
        /// <summary>
        /// Konstruktor koji prima opcije konfiguracije za DbContext.
        /// </summary>
        /// <param name="options">Opcije konfiguracije DbContext-a.</param>
        public EdunovaContext(DbContextOptions<EdunovaContext>options): base(options)
        { 

        }

        /// <summary>
        /// DbSet za entitet <see cref="Lokacija"/>.
        /// </summary>
        public DbSet<Lokacija> Lokacije { get; set; }

        /// <summary>
        /// DbSet za entitet <see cref="Analiticar"/>.
        /// </summary>
        public DbSet<Analiticar> Analiticari { get; set; }

        /// <summary>
        /// DbSet za entitet <see cref="Uzorcitla"/>.
        /// </summary>
        public DbSet<Uzorcitla> UzorciTla {  get; set; }

        /// <summary>
        /// DbSet za entitet <see cref="Analiza"/>.
        /// </summary>
        public DbSet<Analiza> Analize {  get; set; }

        /// <summary>
        /// DbSet za entitet <see cref="Operater"/>.
        /// </summary>
        public DbSet<Operater> Operateri { get; set; }

        /// <summary>
        /// Metoda koja konfigurira model i odnose između entiteta u bazi podataka.
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder koji se koristi za konfiguraciju entiteta.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Operater>().ToTable("operateri");

            // Lokacija 1 - N UzorakTla
            modelBuilder.Entity<Uzorcitla>()
                .HasOne(u => u.Lokacija)
                .WithMany(l => l.UzorciTla)
                .HasForeignKey(u => u.LokacijaId);

            // UzorakTla 1 - N Analiza
            modelBuilder.Entity<Analiza>()
                .HasOne(a => a.UzorakTla)
                .WithMany(u => u.Analize)
                .HasForeignKey(a => a.UzorakTlaId);

            // Analiticar 1 - N Analiza
            modelBuilder.Entity<Analiza>()
                .HasOne(a => a.Analiticar)
                .WithMany(an => an.Analize)
                .HasForeignKey(a => a.AnaliticarId);
        }



    }
}
