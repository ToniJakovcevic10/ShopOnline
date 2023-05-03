using Blazored.LocalStorage;
using ShopOnlineModels.Dtos;
using ShopOnlineWeb.Services.Contracts;

namespace ShopOnlineWeb.Services
{
    public class ManageProductsLocalStorageService : IManageProductsLocalStorageService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IProductService _productService;

        private const string key = "ProductCollection";

        public ManageProductsLocalStorageService(ILocalStorageService localStorageService,
                                                    IProductService productService)
        {
            this._localStorageService = localStorageService;
            this._productService = productService;
        }

        public async Task<IEnumerable<ProductDto>> GetCollection()
        {
            return await this._localStorageService.GetItemAsync<IEnumerable<ProductDto>>(key)
                    ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await this._localStorageService.RemoveItemAsync(key);
        }

        private async Task<IEnumerable<ProductDto>> AddCollection()
        {
            var productCollection = await this._productService.getItems();
            if (productCollection != null)
            {
                await this._localStorageService.SetItemAsync(key, productCollection);
            }

            return productCollection;
        }
    }
}
