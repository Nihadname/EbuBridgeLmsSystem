﻿using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Persistance.Processors;
using LearningManagementSystem.Core.Entities.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EbuBridgeLmsSystem.Persistance.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        private readonly IServiceProvider _serviceProvider;

        public ApplicationDbContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options)
        {
            _serviceProvider = serviceProvider;
        }
        private IAuditLogProcessor AuditLogProcessor => _serviceProvider.GetRequiredService<IAuditLogProcessor>();

        public DbSet<Course> Courses { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Parent> parents { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Lesson> lessons { get; set; }
        public DbSet<LessonStudentTeacher> lessonsStudents { get; set; }
        public DbSet<LessonUnitMaterial> lessonUnitMaterials { get; set; }
        
        public DbSet<LessonUnitVideo> lessonUnitVideos { get; set; }
        public DbSet<Note> notes { get; set; }
        public DbSet<RequestToRegister> requestToRegister { get; set; }
        public DbSet<Fee> fees { get; set; }
        public DbSet<Report> reports { get; set; }
        public DbSet<ReportOption> reportOptions { get; set; }
        public DbSet<CourseStudent> courseStudents { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<TeacherFacultyDegree> TeacherFacultyDegrees { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Language> languages { get; set; }
        public DbSet<CourseImageOutBox> courseImageOutBoxes { get; set; }
        public DbSet<LessonUnit> lessonUnits { get; set; }
        public DbSet<LessonUnitAttendance> lessonUnitAttendances { get; set; }
        public DbSet<LessonUnitStudentHomework> lessonUnitStudentHomeworks { get; set; }
        public DbSet<LessonHomeworkLink> lessonHomeworkLinks { get; set; }
        public DbSet<LessonUnitStudentHomeworkMaterial> lessonUnitStudentHomeworkMaterials { get; set; }
        public DbSet<CourseStudentApprovalOutBox> courseStudentApprovalOutBoxes { get; set; }
        public DbSet<LessonUnitAssignment> lessonUnitAssignments { get; set; }
        public DbSet<LessonStudentStudentApprovalOutBox> LessonStudentStudentApprovalOutBoxes { get; set; }
        public DbSet<CourseTeacherLesson> CourseTeacherLessons { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<CourseLanguage> CourseLanguages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //.IgnoreQueryFilters() ne vaxtsa bu disable olmalidiki is deleted true ve false olanlari gotursun
            base.OnModelCreating(builder);
            builder.Ignore<IdentityUserClaim<Guid>>();
            builder.Ignore<IdentityRoleClaim<Guid>>();
            builder.Ignore<IdentityUserToken<Guid>>();
            builder.Ignore<IdentityUserLogin<Guid>>();
            builder.Ignore<IdentityUserRole<Guid>>();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            await AuditLogProcessor.HandleAuditLogs(entries);

            foreach (var entry in entries)
            {

                if (entry.State == EntityState.Added)
                {
                    entry.Property(s => s.CreatedTime).CurrentValue = DateTime.UtcNow;
                }
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedTime = DateTime.UtcNow;
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