using Microsoft.EntityFrameworkCore;
using UniversityTool.Domain.Models;

namespace UniversityTool.DataBase.Context
{
    public class UniversityToolDbContext : DbContext
    {
        public DbSet<Departament> Departaments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }

        public UniversityToolDbContext(DbContextOptions<UniversityToolDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departament>(d =>
            {
                d.Property(x => x.DateAdded).HasDefaultValueSql("GETDATE()").IsRequired(true);
                d.HasKey(x => x.Id).IsClustered(true);
                d.Property(x => x.Title).IsRequired();

                d.HasMany(x => x.Groups)
                    .WithOne(x => x.Departament)
                    .HasForeignKey(x => x.DepartamentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Group>(g =>
            {
                g.Property(x => x.DateAdded).HasDefaultValueSql("GETDATE()").IsRequired(true);
                g.HasKey(x => x.Id).IsClustered(true);
                g.Property(x => x.Title).IsRequired();

                g.HasMany(x => x.Students)
                    .WithOne(x => x.Group)
                    .HasForeignKey(x => x.GroupId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Student>(s =>
            {
                s.Property(x => x.DateAdded).HasDefaultValueSql("GETDATE()").IsRequired(true);
                s.HasKey(x => x.Id).IsClustered(true);
                s.Property(x => x.Name).IsRequired();
                s.Property(x => x.SecondName).IsRequired();
                s.Property(x => x.ThirdName).IsRequired();

                s.HasOne(x => x.Group)
                    .WithMany(x => x.Students)
                    .HasForeignKey(x => x.GroupId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
