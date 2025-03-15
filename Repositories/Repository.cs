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


        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T?> Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {

            IQueryable<T> query = tracked ? _dbSet.Where(filter) : _dbSet.Where(filter).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
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
