using E_commerce.Helpers;
using E_commerce.Models;
using E_commerce.Models.DTO;
using E_commerce.Models.Extentions;
using E_commerce.Models.IdentityEntities;
using E_commerce.UI.ServicesContracts;
using RepositoriesContracts;
using System.Linq.Expressions;

namespace E_commerce.UI.Services
{
    public class ApplicationUsersService : IApplicationUsersService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationUsersService(IUnitOfWork categoriesRepository)
        {
            _unitOfWork = categoriesRepository;
        }


        public async Task<List<ApplicationUser>> GetAllApplicationUsers()
        {
            return (await _unitOfWork.ApplicationUsers.GetAll()).Select(ApplicationUser => ApplicationUser).ToList();
        }

        public async Task<ApplicationUser> Create(ApplicationUser? applicationUser)
        {
            ValidationHelper.Validate(applicationUser);

            ApplicationUser applicationUserResponse = (await _unitOfWork.ApplicationUsers.Create(applicationUser!));
            return applicationUserResponse;
        }


        public async Task<ApplicationUser?> GetApplicationUserByEmail(string email)
        {
            ApplicationUser? applicationUserResponse = (await _unitOfWork.ApplicationUsers.Get(user => user.Email == email));
            return applicationUserResponse;

        }



        public async Task<ApplicationUser?> GetApplicationUserById(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            ApplicationUser? applicationUserResponse = (await _unitOfWork.ApplicationUsers.Get(user => user.Id == id.GetValueOrDefault()));
            return applicationUserResponse;

        }



        public async Task<ApplicationUser?> DeleteApplicationUserById(Guid? id)
        {
            if (id == null)
                return null;
            ApplicationUser? applicationUser = await _unitOfWork.ApplicationUsers.Get(user => user.Id == id);
            if (applicationUser is null)
            {
                return null;
            }
            var response = _unitOfWork.ApplicationUsers.Delete(applicationUser);
            if (response is null)
                return null;

            await _unitOfWork.CompleteAsync();
            return response;
        }
    }
}
