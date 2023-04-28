using Microsoft.AspNetCore.Components;
using ShopOnlineModels.Dtos;
using ShopOnlineWeb.Services.Contracts;
using ShopOnlineWeb.Shared;

namespace ShopOnlineWeb.Pages
{
    public class ProductsBase:ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }

        [Inject]
        public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        
        public IEnumerable<ProductDto> Products { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await clearLocalStorage();

                Products = await ManageProductsLocalStorageService.GetCollection();
                var shoppingCartItems = await ManageCartItemsLocalStorageService.GetCollection();

                var totalQuantity = shoppingCartItems.Sum(i => i.Quantity);
                ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQuantity);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected IOrderedEnumerable<IGrouping<int, ProductDto>> getGroupedProductsByCategory()
        {
            return from product in Products
                   group product by product.CategoryId into productsByCategoryGroup
                   orderby productsByCategoryGroup.Key
                   select productsByCategoryGroup;
        }

        protected string getCategoryName(IGrouping<int, ProductDto> groupedProductDtos)
        {
            return groupedProductDtos.FirstOrDefault(pg => pg.CategoryId == groupedProductDtos.Key).CategoryName;
        }

        private async Task clearLocalStorage()
        {
            await ManageCartItemsLocalStorageService.RemoveCollection();
            await ManageProductsLocalStorageService.RemoveCollection();
        }
    }
}
