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
        public DbSet<UserSubjects> UserSubjects { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<DocumentLession> DocumentLessions { get; set; }
        public DbSet<Lession> Lessions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<QuestionExam> QuestionExams { get; set; }
        public DbSet<AnswerExam> AnswerExams { get; set; }
        public DbSet<ExaminationQuestion> ExaminationQuestions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<DocumentLession>()
                .HasKey(dl => new { dl.DocumentId, dl.LessionId });

            builder.Entity<UserSubjects>()
                .HasKey(dl => new { dl.UserId, dl.SubjectId });

            builder.Entity<UserNotification>()
                .HasKey(dl => new { dl.UserId, dl.NotificationId });

            builder.Entity<ExaminationQuestion>()
               .HasKey(dl => new { dl.ExaminationId, dl.QuestionExamId });

            builder.Entity<RolePermission>()
               .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Department)
                .WithMany()
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Subject>()
                .HasOne(s => s.Department)
                .WithMany()
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Question>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Answer>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

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
