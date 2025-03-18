using E_commerce.Models.IdentityEntities;

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
