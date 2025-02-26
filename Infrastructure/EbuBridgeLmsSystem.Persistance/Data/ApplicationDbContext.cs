using EbuBridgeLmsSystem.Domain.Entities;
using LearningManagementSystem.Core.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EbuBridgeLmsSystem.Persistance.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Parent> parents { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Lesson> lessons { get; set; }
        public DbSet<LessonStudent> lessonsStudents { get; set; }
        public DbSet<LessonMaterial> lessonsMaterial { get; set; }
        public DbSet<LessonQuiz> lessonQuizzes { get; set; }
        public DbSet<LessonVideo> lessonsVideo { get; set; }
        public DbSet<Note> notes { get; set; }
        public DbSet<QuizOption> quizOptions { get; set; }
        public DbSet<QuizQuestion> quizQuestions { get; set; }
        public DbSet<QuizResult> quizResults { get; set; }
        public DbSet<RequestToRegister> requestToRegister { get; set; }
        public DbSet<Fee> fees { get; set; }
        public DbSet<Report> reports { get; set; }
        public DbSet<ReportOption> reportOptions { get; set; }
        public DbSet<CourseStudent> courseStudents { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<TeacherFacultyDegree> TeacherFacultyDegrees { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }
        public  DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            base.OnModelCreating(builder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(s => s.CreatedTime).CurrentValue = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(s => s.UpdatedTime).CurrentValue = DateTime.UtcNow;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
