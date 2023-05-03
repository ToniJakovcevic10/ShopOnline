using Microsoft.AspNetCore.Components;
using ShopOnlineModels.Dtos;
using ShopOnlineWeb.Services.Contracts;

namespace ShopOnlineWeb.Pages
{
    public class ProductsByCategoryBase:ComponentBase
    {
        [Parameter]
        public int categoryId { get; set; }
        [Inject]
        public IProductService _productService { get; set; }
        [Inject]
        public IManageProductsLocalStorageService _manageProductsLocalStorageService { get; set; }
        public IEnumerable<ProductDto> products { get; set; }

        public string CategoryName { get; set; }
        public string errorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                products = await _productService.getItemsByCategory(categoryId);
                if(products != null && products.Count()>0)
                {
                    var productDto = products.FirstOrDefault(p=>p.CategoryId == categoryId);
                    if(productDto != null)
                    {
                        CategoryName = productDto.CategoryName;

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        private async Task<IEnumerable<ProductDto>> GetProductCollectionByCategoryId(int categoryId)
        {
            var productCollection = await _manageProductsLocalStorageService.GetCollection();
            if(productCollection!=null)
            {
                return productCollection.Where(p => p.CategoryId == categoryId); 
            }
            else
            {
                return await _productService.getItemsByCategory(categoryId);
            }
        }
    }
}
