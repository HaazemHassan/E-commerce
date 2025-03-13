using E_commerce.DataAccess.Data;
using E_commerce.Models;
using Microsoft.EntityFrameworkCore;
using RepositoriesContracts;

namespace Repositories
{
    public class CompaniesRepository : Repository<Company>, ICompaniesRepository
    {
        private readonly ApplicationDbContext _db;

        public CompaniesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Company?> Update(Company Company)
        {
            Company? oldCompany = await _db.Companies.FirstOrDefaultAsync(c => c.Id == Company.Id);
            if (oldCompany == null)
                return null;

            bool isNameTaken = await _db.Companies.AnyAsync(c => c.Name == Company.Name && c.Id != Company.Id);
            if (isNameTaken)
                return null;
                //throw new ArgumentException("Company name is already taken by another Company");


            //better than _db.Update(Company)
            //because this will update only the changed vaues
            _db.Entry(oldCompany).CurrentValues.SetValues(Company);
            return Company;

        }

    }
}
