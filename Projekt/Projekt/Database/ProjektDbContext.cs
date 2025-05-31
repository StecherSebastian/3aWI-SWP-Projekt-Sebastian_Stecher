using Microsoft.EntityFrameworkCore;
using Projekt.Models;

namespace Projekt.Database
{
    public class ProjektDbContext : DbContext
    {
        public DbSet<School> Schools { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Student> Students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "ProjektDB.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
            optionsBuilder.LogTo(log => {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(log);
                Console.ResetColor();
            }, LogLevel.Information);
        }
    }
}