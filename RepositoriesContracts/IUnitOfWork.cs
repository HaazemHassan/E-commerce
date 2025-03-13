using System.Threading.Tasks;

namespace RepositoriesContracts
{
    public interface IUnitOfWork
    {
        ICategoriesRepository Categories { get; } 
        IProductsRepository Products { get; }

        ICompaniesRepository Companies { get; }
        IShoppingCartsRepository ShoppingCarts { get; }

        IApplicationUserRepository ApplicationUsers { get; }

        Task CompleteAsync();
    }
}
