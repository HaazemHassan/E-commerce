using AutoMapper;
using E_commerce.Models.DTO;

namespace E_commerce.Models.Extentions
{
    public static class ProductExtensions
    {
        //we should use auto mapper in the services and cotnrollers instead of using all those methods
        //but for time being i will use it here
        public static Product ToProduct(this ProductAddRequest productAddRequest, IMapper mapper)
        {
            return mapper.Map<Product>(productAddRequest);
        }


        public static Product ToProduct(this ProductUpdateRequest product, IMapper mapper)
        {
            return mapper.Map<Product>(product);

        }


        public static ProductResponse ToProductResponse(this Product product, IMapper mapper)
        {
            return mapper.Map<ProductResponse>(product);

        }

        public static ProductUpdateRequest ToProductUpdateRequest(this ProductResponse product, IMapper mapper)
        {
            return mapper.Map<ProductUpdateRequest>(product);

        }
    }

}
