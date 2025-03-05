using System.Threading.Tasks;

namespace RepositoriesContracts
{
    public interface IUnitOfWork
    {
        ICategoriesRepository Categories { get; } 
        IProductsRepository Products { get; } 

        Task CompleteAsync();
    }
}
