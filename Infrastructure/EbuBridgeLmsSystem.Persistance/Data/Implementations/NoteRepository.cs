using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class NoteRepository : Repository<Note>, INoteRepository
    {
        public NoteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
