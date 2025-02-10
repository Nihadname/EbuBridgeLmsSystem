using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public interface IUnitOfWork
    {
        public ICourseRepository CourseRepository { get; }
        public IStudentRepository StudentRepository { get; }
        public ITeacherRepository TeacherRepository { get; }
        public IParentRepository ParentRepository { get; }
        public Task Commit();
        Task RollbackTransactionAsync();
        public Task<IDbContextTransaction> BeginTransactionAsync();
        public IRequstToRegisterRepository RequstToRegisterRepository { get; }
        public ICourseRepository courseRepository { get; }
        public INoteRepository NoteRepository { get; }
        public IReportRepository ReportRepository { get; }
        public IReportOptionRepository ReportOptionRepository { get; }
        public IAddressRepository AddressRepository { get; }
        public IFeeRepository FeeRepository { get; }

    }
}
