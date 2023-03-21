using Microsoft.EntityFrameworkCore;
using UniversityTool.Domain.Models;

namespace UniversityTool.DataBase.Context
{
    public class UniversityToolDbContext : DbContext
    {
        public DbSet<Departament> Departaments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }

        public UniversityToolDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departament>(d =>
            {
                d.HasKey(x => x.Id);
                d.Property(x => x.Title).IsRequired();

                d.HasMany(x => x.Groups)
                    .WithOne(x => x.Departament)
                    .HasForeignKey(x => x.DepartamentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Group>(g =>
            {
                g.HasKey(x => x.Id);
                g.Property(x => x.Title).IsRequired();

                g.HasMany(x => x.Students)
                    .WithOne(x => x.Group)
                    .HasForeignKey(x => x.GroupId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Student>(s =>
            {
                s.HasKey(x => x.Id);
                s.Property(x => x.Name).IsRequired();
                s.Property(x => x.SecondName).IsRequired();
                s.Property(x => x.ThirdName).IsRequired();

                s.HasOne(x => x.Group)
                    .WithMany(x => x.Students)
                    .HasForeignKey(x => x.GroupId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
