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
    }
}
