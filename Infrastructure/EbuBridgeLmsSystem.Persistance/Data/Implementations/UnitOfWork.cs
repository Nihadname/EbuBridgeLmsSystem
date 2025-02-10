using EbuBridgeLmsSystem.Domain.Repositories;
using EbuBridgeLmsSystem.Persistance.Data;
using LearningManagementSystem.Core.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _applicationDbContext;

        public ICourseRepository CourseRepository { get; private set; }
        public IStudentRepository StudentRepository { get; private set; }
        public ITeacherRepository TeacherRepository { get; private set; }
        public IParentRepository ParentRepository { get; private set; }
        public IRequstToRegisterRepository RequstToRegisterRepository { get; private set; }
        public ICourseRepository courseRepository { get; private set; }
        public INoteRepository NoteRepository { get; private set; }
        public IReportRepository ReportRepository { get; private set; }
        public IReportOptionRepository ReportOptionRepository { get; private set; }
        public IAddressRepository AddressRepository { get; private set; }
        public IFeeRepository FeeRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            CourseRepository = new CourseRepository(applicationDbContext);
            StudentRepository = new StudentRepository(applicationDbContext);
            TeacherRepository = new TeacherRepository(applicationDbContext);
            ParentRepository = new ParentRepository(applicationDbContext);
            RequstToRegisterRepository = new RequstToRegisterRepository(applicationDbContext);
            CourseRepository = new CourseRepository(applicationDbContext);
            NoteRepository = new NoteRepository(applicationDbContext);
            ReportRepository = new ReportRepository(applicationDbContext);
            ReportOptionRepository = new ReportOptionRepository(applicationDbContext);
            AddressRepository = new AddressRepository(applicationDbContext);
            FeeRepository = new FeeRepository(applicationDbContext);
            _applicationDbContext = applicationDbContext;

        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _applicationDbContext.Database.BeginTransactionAsync();
        }
        public async Task RollbackTransactionAsync()
        {
            await _applicationDbContext.Database.RollbackTransactionAsync();
        }

        public async Task Commit()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}
