using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
