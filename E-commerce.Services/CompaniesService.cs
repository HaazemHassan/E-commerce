using E_commerce.Helpers;
using E_commerce.Models;
using E_commerce.UI.ServicesContracts;
using RepositoriesContracts;

namespace E_commerce.UI.Services
{
    public class CompaniesService : ICompaniesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompaniesService(IUnitOfWork CompaniesRepository)
        {
            _unitOfWork = CompaniesRepository;
        }


        public async Task<List<Company>> GetAllCompanies()
        {
            return (await _unitOfWork.Companies.GetAll());
        }

        public async Task<Company> Create(Company? company)
        {
            ValidationHelper.Validate(company);

            Company createdCompany = (await _unitOfWork.Companies.Create(company!));
            await _unitOfWork.CompleteAsync();
            return createdCompany;
        }


        public async Task<Company?> GetCompanyByName(string name)
        {
            Company? Company = (await _unitOfWork.Companies.Get(c => c.Name == name));
            return Company;

        }



        public async Task<Company?> GetCompanyById(int? id)
        {
            if (id == null)
            {
                return null;
            }

            Company? Company = (await _unitOfWork.Companies.Get(c => c.Id == id.GetValueOrDefault()));
            return Company;

        }

        public async Task<Company?> UpdateCompany(Company? Company)
        {
            ValidationHelper.Validate(Company);

            var response = await _unitOfWork.Companies.Update(Company!);
            if (response is null)
                return null;
            await _unitOfWork.CompleteAsync();

            return response;

        }

        public async Task<Company?> DeleteCompanyById(int? id)
        {
            if (id == null)
                return null;
            Company? Company = await _unitOfWork.Companies.Get(c => c.Id == id);
            if (Company is null)
            {
                return null;
            }
            var response = _unitOfWork.Companies.Delete(Company);
            if (response is null)
                return null;

            await _unitOfWork.CompleteAsync();
            return response;
        }
    }
}
