using E_commerce.Models;
using E_commerce.Models.DTO;
using E_commerce.Models.IdentityEntities;
using System.Linq.Expressions;

namespace E_commerce.UI.ServicesContracts
{
    
    public interface IApplicationUsersService
    {
        public Task<List<ApplicationUser>> GetAllApplicationUsers();
        public Task<ApplicationUser> Create(ApplicationUser applicationUser);

        public Task<ApplicationUser?> GetApplicationUserById(Guid? id);

        public Task<ApplicationUser?> DeleteApplicationUserById(Guid? id);
        public Task<ApplicationUser?> GetApplicationUserByEmail(string email);

    }
}
