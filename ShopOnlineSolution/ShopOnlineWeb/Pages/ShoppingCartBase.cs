using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopOnlineModels.Dtos;
using ShopOnlineWeb.Services.Contracts;
using ShopOnlineWeb.Shared;

namespace ShopOnlineWeb.Pages
{
    public class ShoppingCartBase:ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        public List<CartItemDto> ShoppingCartItems { get; set; }

        public string ErrorMessage { get; set; }
        protected string TotalPrice { get; set; }
        protected int TotalQuantity { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                CalculateCartSummaryTotals();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        protected async Task DeleteCartItem_Click(int id)
        {
            var cartItemDto = await ShoppingCartService.DeleteItem(id);
            RemoveCartItem(id);
            CalculateCartSummaryTotals();
        }
        
        protected async Task UpdateQuantityCartItem_Click(int id, int quantity)
        {
            try
            {
                if(quantity>0)
                {
                    var updateItemDto = new CartItemQuantityUpdateDto
                    {
                        CartItemId = id,
                        Quantity = quantity
                    };
                    var returnedUpdateItemDto = await this.ShoppingCartService.UpdateQuantity(updateItemDto);
                    updateItemTotalPrice(returnedUpdateItemDto);
                    CalculateCartSummaryTotals();

                    await Js.InvokeVoidAsync("MakeUpdateQuantityButtonVisible", id, false);
                }
                else
                {
                    var item = this.ShoppingCartItems.FirstOrDefault(i => i.Id == id);
                    if(item!=null)
                    {
                        item.Quantity = 1;
                        item.TotalPrice = item.Price;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected async Task UpdateQuantity_Input(int id)
        {
            await Js.InvokeVoidAsync("MakeUpdateQuantityButtonVisible", id, true);

        }

        private void updateItemTotalPrice(CartItemDto cartItemDto)
        {
            var item = GetCartItem(cartItemDto.Id);
            if(item!=null)
            {
                item.TotalPrice = cartItemDto.Price*cartItemDto.Quantity;
            }
        }

        private void CalculateCartSummaryTotals()
        {
            setTotalPrice();
            setTotalQuantity();
        }
        private void setTotalPrice()
        {
            TotalPrice = this.ShoppingCartItems.Sum(p=>p.TotalPrice).ToString("C");
        }
        private void setTotalQuantity()
        {
            TotalQuantity = this.ShoppingCartItems.Sum(p => p.Quantity);
        }

        private CartItemDto GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(x => x.Id == id);
        }
        private void RemoveCartItem(int id)
        {
            var cartItemDto = GetCartItem(id);

            ShoppingCartItems.Remove(cartItemDto);
        }

    }
}
