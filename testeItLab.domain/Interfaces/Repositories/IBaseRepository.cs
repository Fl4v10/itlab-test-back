using System.Linq;
using System.Threading.Tasks;

namespace testeItLab.domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);

        void Add(TEntity entity);

        void Delete(TEntity entity);

        Task DeleteAsync(TEntity entity);

        IQueryable<TEntity> Query();

        Task UpdateAsync(TEntity entity);

        void Update(TEntity entity);

        Task SaveChangeAsync();
    }
}
