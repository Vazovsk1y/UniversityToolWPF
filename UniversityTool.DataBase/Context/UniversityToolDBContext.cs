using Microsoft.EntityFrameworkCore;
using UniversityTool.Domain.Models;

namespace UniversityTool.DataBase.Context
{
    public class UniversityToolDBContext : DbContext
    {
        public DbSet<Departament> Departaments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departament>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Group>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<Student>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Departament>()
                .HasMany(f => f.Groups)
                .WithOne(g => g.Departament)
                .HasForeignKey(g => g.Id);

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Students)
                .WithOne(s => s.Group)
                .HasForeignKey(s => s.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UniversityToolDB;Trusted_Connection=True;");
        }
    }
}
