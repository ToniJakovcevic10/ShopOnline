using ShopOnlineModels.Dtos;

namespace ShopOnlineWeb.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> getItems();
    }
}
