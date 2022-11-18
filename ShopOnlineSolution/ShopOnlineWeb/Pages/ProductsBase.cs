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


    }
}
