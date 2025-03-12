using E_commerce.Models;

namespace E_commerce.UI.ServicesContracts
{
    public interface ICompaniesService
    {
        public Task<List<Company>> GetAllCompanies();
        public Task<Company> Create(Company Company);

        public Task<Company?> GetCompanyById(int? id);

        public Task<Company?> UpdateCompany(Company Company);

        public Task<Company?> DeleteCompanyById(int? id);
        public Task<Company?> GetCompanyByName(string name);


    }
}
