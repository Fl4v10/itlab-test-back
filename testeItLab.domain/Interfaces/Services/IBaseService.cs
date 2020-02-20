using System.Linq;
using System.Threading.Tasks;

namespace testeItLab.domain.Interfaces.Services
{

    public interface IBaseService<TEntity, TKey1>
        where TEntity : class, new()
        where TKey1 : struct
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task DeleteAsync(TKey1 key1);
        Task<TEntity> GetAsync(TKey1 key1);
        IQueryable<TEntity> GetList();
        Task<TEntity> UpdateAsync(TKey1 key1, TEntity updateEntity);
    }
}
