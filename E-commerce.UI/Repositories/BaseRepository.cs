//using E_commerce.UI.Data;
//using Microsoft.EntityFrameworkCore;
//using System.Linq.Expressions;

//namespace E_commerce.UI.Repositories
//{
//    public abstract class BaseRepository<T> where T : class
//    {
//        protected readonly ApplicationDbContext _context;
//        protected readonly DbSet<T> _dbSet;

//        public BaseRepository(ApplicationDbContext context)
//        {
//            _context = context;
//            _dbSet = context.Set<T>();
//        }

//        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
//        {
//            return await _dbSet.AnyAsync(predicate);
//        }
//    }

//}
