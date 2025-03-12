using E_commerce.Models;
using E_commerce.Models.Models;

namespace RepositoriesContracts
{
	public interface ICompaniesRepository : IRepository<Company>
	{
		public Task<Company?> UpdateCompany(Company Company);

	}
}
