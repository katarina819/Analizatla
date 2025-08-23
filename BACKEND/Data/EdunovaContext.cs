using System.Text.RegularExpressions;
using BACKEND.Models;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Data
{
    public class EdunovaContext : DbContext
    {

        public EdunovaContext(DbContextOptions<EdunovaContext>options): base(options)
        { 

        }

        public DbSet<Lokacija> Lokacije { get; set; }
        public DbSet<Analiticar> Analiticari { get; set; }

        public DbSet<Uzorcitla> UzorciTla {  get; set; }
        public DbSet<Analiza> Analize {  get; set; }

        public DbSet<Operater> Operateri { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
