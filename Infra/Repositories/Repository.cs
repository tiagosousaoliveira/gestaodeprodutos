using Domain.Persistence;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public abstract class  Repository : IRepository
    {
        protected DatabaseContext Context { get; }
        public Repository(DatabaseContext context)
        {
            Context = context;                
        }
        public Task CommitAsync() => Context.SaveChangesAsync();
    }
}
