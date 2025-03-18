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
        IOrdersRepository Orders { get; }
        IOrdersDetailRepository OrdersDetail { get; }

        Task CompleteAsync();
    }
}
