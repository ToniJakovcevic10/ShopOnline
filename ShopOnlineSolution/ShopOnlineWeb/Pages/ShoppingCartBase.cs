using Microsoft.AspNetCore.Components;
using ShopOnlineModels.Dtos;
using ShopOnlineWeb.Services.Contracts;
using ShopOnlineWeb.Shared;

namespace ShopOnlineWeb.Pages
{
    public class ShoppingCartBase:ComponentBase
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        public List<CartItemDto> ShoppingCartItems { get; set; }

        public string ErrorMessage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
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
