using E_commerce.Models;
using E_commerce.Models.IdentityEntities;
using System.Linq.Expressions;

namespace RepositoriesContracts
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
    }
}
