using E_commerce.DataAccess.Data;
using E_commerce.Models;
using Microsoft.EntityFrameworkCore;
using RepositoriesContracts;

namespace Repositories
{
    public class ProductsRepository : Repository<Product>, IProductsRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Product?> Update(Product product)
        {
            Product? oldProduct = await _db.Products.FirstOrDefaultAsync(c => c.Id == product.Id);
            if (oldProduct == null)
                return null;

            _db.Entry(oldProduct).CurrentValues.SetValues(product);
            return product;

        }

    }
}
