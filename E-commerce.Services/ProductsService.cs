using E_commerce.Helpers;
using E_commerce.Models;
using E_commerce.Models.DTO;
using E_commerce.Models.Extentions;
using E_commerce.UI.ServicesContracts;
using RepositoriesContracts;

namespace E_commerce.UI.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsService(IUnitOfWork categoriesRepository)
        {
            _unitOfWork = categoriesRepository;
        }


        public async Task<List<ProductResponse>> GetAllProducts(string? includeProperties = null)
        {
            return (await _unitOfWork.Products.GetAll(includeProperties:includeProperties)).Select(Product => Product.ToProductResponse()).ToList();
        }

        public async Task<ProductResponse?> Create(ProductAddRequest? productAddRequest)
        {
            ValidationHelper.Validate(productAddRequest);

            //check if ISBN not exist (ISBN is unqiue)
            Product? product = await _unitOfWork.Products.Get(p => p.ISBN == productAddRequest!.ISBN);
            if (product is not null)
                return null;

            //Category that the user selected must be exists
            Category? category = await _unitOfWork.Categories.Get(c => c.Id == productAddRequest!.CategoryId);
            if (category is null) return null;

            ProductResponse productResponse = (await _unitOfWork.Products.Create(productAddRequest!.ToProduct())).ToProductResponse();
            await _unitOfWork.CompleteAsync();
            return productResponse;
        }


        public async Task<ProductResponse?> GetProductByTitle(string title, string? includeProperties = null)
        {
            ProductResponse? productResponse = (await _unitOfWork.Products.Get(c => c.Title == title,includeProperties))?.ToProductResponse();
            return productResponse;

        }



        public async Task<ProductResponse?> GetProductById(int? id, string? includeProperties = null)
        {
            if (id == null)
            {
                return null;
            }

            ProductResponse? productResponse = (await _unitOfWork.Products.Get(c => c.Id == id.GetValueOrDefault(),includeProperties))?.ToProductResponse();
            return productResponse;

        }

        public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest? product)
        {
            ValidationHelper.Validate(product);

            ProductResponse? productResponse = await GetProductById(product!.Id);

            //no product exits to update !!!!
            if (productResponse is null)
                return null;

            //if the user change the ISBN by someway, we should ignore the updated value and keep the old one
            product.ISBN = productResponse.ISBN;


            //in the edit view if the user didn't supply the image we should keep the old one
            if(string.IsNullOrEmpty(product.ImageUrl))
                product.ImageUrl = productResponse.ImageUrl;

            var response = await _unitOfWork.Products.Update(product!.ToProduct());
            if (response is null)
                return null;
            await _unitOfWork.CompleteAsync();

            return response.ToProductResponse();

        }

        public async Task<ProductResponse?> DeleteProductById(int? id)
        {
            if (id == null)
                return null;
            Product? product = await _unitOfWork.Products.Get(c => c.Id == id);
            if (product is null)
            {
                return null;
            }
            var response = _unitOfWork.Products.Delete(product);
            if (response is null)
                return null;

            await _unitOfWork.CompleteAsync();
            return response.ToProductResponse();
        }

        public async Task<ProductResponse?> GetProductByISBN(string ISBN, string? includeProperties = null)
        {
            ISBN = ISBN.Trim().ToLower();
            ProductResponse? productResponse = (await _unitOfWork.Products.Get(c => c.ISBN == ISBN,includeProperties))?.ToProductResponse();
            return productResponse;
        }
    }
}
