using E_commerce.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using RepositoriesContracts;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task<T> Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }


        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            return filter is null ? await _dbSet.ToListAsync() : await _dbSet.Where(filter).ToListAsync();
        }

        public async Task<T?> Get(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.FirstOrDefaultAsync(filter);
        }


        public T? Delete(T entity)
        {
            _dbSet.Remove(entity);
            return entity;
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            _db.RemoveRange(entity);
        }
    }
}
