using Microsoft.AspNetCore.Components;
using ShopOnlineModels.Dtos;
using ShopOnlineWeb.Services.Contracts;

namespace ShopOnlineWeb.Pages
{
    public class ProductsBase:ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Products = await ProductService.getItems();
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
    }
}
