using E_commerce.DataAccess.Data;
using RepositoriesContracts;

namespace Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private readonly ApplicationDbContext _db;

        public ICategoriesRepository Categories { get; private set; }
        public IProductsRepository Products { get; private set; }
        public ICompaniesRepository Companies { get; private set; }



        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Categories = new CategoriesRepository(_db);
            Products = new ProductsRepository(_db);
            Companies = new CompaniesRepository(_db);
        }

        public async Task CompleteAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
