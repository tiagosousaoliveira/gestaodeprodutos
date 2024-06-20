using System.Threading.Tasks;

namespace Domain.Persistence
{
    public interface IRepository
    {
        Task CommitAsync();
    }
}
