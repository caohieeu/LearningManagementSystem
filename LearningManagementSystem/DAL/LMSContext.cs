using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LearningManagementSystem.DAL
{
    public class LMSContext : IdentityDbContext<ApplicationUser>
    {
        public LMSContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<DocumentLession> DocumentLessions { get; set; }
        public DbSet<Lession> Lessions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<DocumentLession>()
                .HasKey(dl => new { dl.DocumentId, dl.LessionId });

            //builder.Entity<Document>()
            //    .HasOne(u => u.FileDetails)
            //    .WithOne(fd => fd.Document)
            //    .HasForeignKey<FileDetails>(u => u.Id); 

            //builder.Entity<ApplicationUser>(entity =>
            //{
            //    entity.ToTable("Users");
            //});

            //builder.Entity<IdentityRole>(entity =>
            //{
            //    entity.ToTable("Roles");
            //});

            //foreach (var entityType in builder.Model.GetEntityTypes())
            //{
            //    var tableName = entityType.GetTableName();
            //    if (tableName.StartsWith("AspNet"))
            //    {
            //        entityType.SetTableName(tableName.Substring(6));
            //    }
            //}
        }
    }
}
