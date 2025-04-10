﻿using LearningManagementSystem.Core.Repositories;

namespace EbuBridgeLmsSystem.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public ICourseRepository CourseRepository { get; }
        public IStudentRepository StudentRepository { get; }
        public ITeacherRepository TeacherRepository { get; }
        public IParentRepository ParentRepository { get; }
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        void Dispose();
        public IRequstToRegisterRepository RequstToRegisterRepository { get; }
        public INoteRepository NoteRepository { get; }
        public IReportRepository ReportRepository { get; }
        public IReportOptionRepository ReportOptionRepository { get; }
        public IAddressRepository AddressRepository { get; }
        public IFeeRepository FeeRepository { get; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }  
        public  ICountryRepository CountryRepository { get; }
        public ICityRepository CityRepository { get; }
        public IAuditLogRepository AuditLogRepository { get; }
        public ILanguageRepository LanguageRepository { get; }
        public ICourseImageOutBoxRepository CourseImageOutBoxRepository { get; }
        public ICourseStudentRepository CourseStudentRepository { get; }
        public ILessonMaterialRepository LessonMaterialRepository { get; }
        public ILessonRepository LessonRepository { get; }
        public ILessonVideoRepository LessonVideoRepository { get; }

    }
}
