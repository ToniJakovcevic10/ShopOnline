using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopOnlineModels.Dtos;
using ShopOnlineWeb.Services.Contracts;
using ShopOnlineWeb.Shared;

namespace ShopOnlineWeb.Pages
{
    public class CheckoutBase:ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }

        [Inject]
        public IManageCartItemsLocalStorageService _manageCartItemsLocalStorageService { get; set; }
        protected IEnumerable<CartItemDto> ShoppingCartItems { get; set; }
        protected int TotalQuantity { get; set; }

        protected string PaymentDescription { get; set; }
        protected decimal PaymentAmount { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await _manageCartItemsLocalStorageService.GetCollection();

                if(ShoppingCartItems != null)
                {
                    Guid orderGuid = Guid.NewGuid();

                    PaymentAmount = ShoppingCartItems.Sum(p=>p.TotalPrice);
                    TotalQuantity = ShoppingCartItems.Sum(p=>p.Quantity);
                    PaymentDescription = $"O_{HardCoded.UserId}_{orderGuid}";

                }
            }
            catch (Exception)
            {
                //log exception
                throw;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if(firstRender)
                {
                    await Js.InvokeVoidAsync("initPayPalButton");
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

    }
}
