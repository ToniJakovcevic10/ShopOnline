using Microsoft.AspNetCore.Components;
using ShopOnlineModels.Dtos;

namespace ShopOnlineWeb.Pages
{
    public class DisplayProductsBase:ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
