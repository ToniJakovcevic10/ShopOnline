using ShopOnlineModels.Dtos;

namespace ShopOnlineWeb.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> getItems();
        Task<ProductDto> getItem(int id);
        Task<IEnumerable<ProductCategoryDto>> getProductCategories();

        Task<IEnumerable<ProductDto>> getItemsByCategory(int categoryId);
    }
}
