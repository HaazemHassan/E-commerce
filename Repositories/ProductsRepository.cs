using E_commerce.DataAccess.Data;
using E_commerce.Models;
using E_commerce.Models.Models;
using Microsoft.EntityFrameworkCore;
using RepositoriesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductsRepository : Repository<Product>, IProductsRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Product?> UpdateProduct(Product product)
        {
            Product? oldProduct = await _db.Products.FirstOrDefaultAsync(c => c.Id == product.Id);
            if (oldProduct == null)
                return null;

            _db.Entry(oldProduct).CurrentValues.SetValues(product);
            return product;

        }

    }
}
