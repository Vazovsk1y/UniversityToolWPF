using Microsoft.EntityFrameworkCore;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Models.Base;

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
                d.Property(x => x.DateAdded).HasDefaultValueSql("GETDATE()").IsRequired();
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
                g.Property(x => x.DateAdded).HasDefaultValueSql("GETDATE()").IsRequired();
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
                s.Property(x => x.DateAdded).HasDefaultValueSql("GETDATE()").IsRequired();
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

        public override int SaveChanges()
        {
            ModifyDateUpdated();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ModifyDateUpdated();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ModifyDateUpdated();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ModifyDateUpdated();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ModifyDateUpdated()
        {
            var updatedEntities = ChangeTracker.Entries().Where(e => e.State is EntityState.Modified);

            foreach(var entry in updatedEntities)
            {
                if (entry.Entity is BaseModel model)
                {
                    model.DateUpdated = DateTime.UtcNow;
                }
            }
        }
    }
}
