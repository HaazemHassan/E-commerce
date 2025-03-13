using E_commerce.Models;

namespace RepositoriesContracts
{
	public interface ICompaniesRepository : IRepository<Company>
	{
		public Task<Company?> Update(Company Company);

	}
}
