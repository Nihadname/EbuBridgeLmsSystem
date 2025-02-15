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

    }
}
