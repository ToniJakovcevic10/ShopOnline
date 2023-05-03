using ShopOnlineModels.Dtos;

namespace ShopOnlineWeb.Services.Contracts
{
    public interface IManageCartItemsLocalStorageService
    {
        Task<List<CartItemDto>> GetCollection();
        Task SaveCollection(List<CartItemDto> cartItemDtos);
        Task RemoveCollection();
    }
}
