using Blazored.LocalStorage;
using ShopOnlineModels.Dtos;
using ShopOnlineWeb.Services.Contracts;
using ShopOnlineWeb.Shared;

namespace ShopOnlineWeb.Services
{
    public class ManageCartItemsLocalStorageService : IManageCartItemsLocalStorageService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IShoppingCartService _shoppingCartService;

        const string key = "CartItemCollection";

        public ManageCartItemsLocalStorageService(ILocalStorageService localStorageService,
                                                    IShoppingCartService shoppingCartService)
        {
            this._localStorageService = localStorageService;
            this._shoppingCartService = shoppingCartService;
        }

        public async Task<List<CartItemDto>> GetCollection()
        {
            return await this._localStorageService.GetItemAsync<List<CartItemDto>>(key)
                    ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
           await this._localStorageService.RemoveItemAsync(key);
        }

        private async Task<List<CartItemDto>> AddCollection()
        {
            var shoppingCartCollection = await this._shoppingCartService.GetItems(HardCoded.UserId);
            if (shoppingCartCollection != null)
            {
                await this._localStorageService.SetItemAsync(key, shoppingCartCollection);
            }

            return shoppingCartCollection;
        }

        public async Task SaveCollection(List<CartItemDto> cartItemDtos)
        {
            await this._localStorageService.SetItemAsync(key,cartItemDtos);
        }

    }
}
