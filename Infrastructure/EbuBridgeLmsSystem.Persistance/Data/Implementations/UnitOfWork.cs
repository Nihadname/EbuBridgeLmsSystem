using EbuBridgeLmsSystem.Domain.Repositories;
using EbuBridgeLmsSystem.Persistance.Data;
using LearningManagementSystem.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _applicationDbContext;
        private bool _disposed;

        public IStudentRepository StudentRepository { get; private set; }
        public ITeacherRepository TeacherRepository { get; private set; }
        public IParentRepository ParentRepository { get; private set; }
        public IRequstToRegisterRepository RequstToRegisterRepository { get; private set; }
        public ICourseRepository CourseRepository { get; private set; }
        public INoteRepository NoteRepository { get; private set; }
        public IReportRepository ReportRepository { get; private set; }
        public IReportOptionRepository ReportOptionRepository { get; private set; }
        public IAddressRepository AddressRepository { get; private set; }
        public IFeeRepository FeeRepository { get; private set; }
        public IRefreshTokenRepository RefreshTokenRepository { get; private set; }
        public ICountryRepository CountryRepository { get; private set; }
        public ICityRepository CityRepository { get; private set; }
        public IAuditLogRepository AuditLogRepository { get; private set; }
        public ILanguageRepository LanguageRepository { get; private set; }
        public ICourseImageOutBoxRepository CourseImageOutBoxRepository { get; private set; }
        public ICourseStudentRepository CourseStudentRepository { get; private set; }
        public ILessonUnitMaterialRepository LessonUnitMaterialRepository { get; private set; }
        public ILessonRepository LessonRepository { get; private set; }
        public ILessonUnitVideoRepository LessonUnitVideoRepository { get; private set; }
        public ILessonStudentRepository LessonStudentRepository { get; private set; }
        public ILessonUnitRepository LessonUnitRepository   { get; private set; }
        public ILessonUnitAttendanceRepository LessonUnitAttendanceRepository { get; private set; }
        public ICourseStudentApprovalOutBoxRepository CourseStudentApprovalOutBoxRepository { get; private set; }
        public ILessonUnitAssignmentRepository LessonUnitAssignmentRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            CourseRepository = new CourseRepository(applicationDbContext);
            StudentRepository = new StudentRepository(applicationDbContext);
            TeacherRepository = new TeacherRepository(applicationDbContext);
            ParentRepository = new ParentRepository(applicationDbContext);
            RequstToRegisterRepository = new RequstToRegisterRepository(applicationDbContext);
            NoteRepository = new NoteRepository(applicationDbContext);
            ReportRepository = new ReportRepository(applicationDbContext);
            ReportOptionRepository = new ReportOptionRepository(applicationDbContext);
            AddressRepository = new AddressRepository(applicationDbContext);
            FeeRepository = new FeeRepository(applicationDbContext);
            RefreshTokenRepository= new RefreshTokenRepository(applicationDbContext);
            CountryRepository = new CountryRepository(applicationDbContext);
            CityRepository = new CityRepository(applicationDbContext);
            AuditLogRepository = new AuditLogRepository(applicationDbContext);
            LanguageRepository = new LanguageRepository(applicationDbContext);
            CourseImageOutBoxRepository = new CourseImageOutBoxRepository(applicationDbContext);
            CourseStudentRepository = new CourseStudentRepository(applicationDbContext);
            LessonUnitMaterialRepository = new LessonUnitMaterialRepository(applicationDbContext);
            LessonRepository=new LessonRepository(applicationDbContext);
            LessonUnitVideoRepository = new LessonUnitVideoRepository(applicationDbContext);
            LessonStudentRepository = new LessonStudentRepository(applicationDbContext);    
            LessonUnitRepository=new LessonUnitRepository(applicationDbContext);
            LessonUnitAttendanceRepository = new LessonUnitAttendanceRepository(applicationDbContext);
            CourseStudentApprovalOutBoxRepository = new CourseStudentApprovalOutBoxRepository(applicationDbContext);
            LessonUnitAssignmentRepository = new LessonUnitAssignmentRepository(applicationDbContext);
            _applicationDbContext = applicationDbContext;

        }
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = _applicationDbContext.Database.CurrentTransaction;
            if (transaction == null)
            {
                await _applicationDbContext.Database.BeginTransactionAsync(cancellationToken);
            }
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = _applicationDbContext.Database.CurrentTransaction;
            if (transaction != null)
            {
                await transaction.CommitAsync();
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = _applicationDbContext.Database.CurrentTransaction;
            if (transaction != null)
            {
                await transaction.RollbackAsync();
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _applicationDbContext.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
