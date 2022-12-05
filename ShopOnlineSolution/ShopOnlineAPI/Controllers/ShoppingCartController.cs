using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnlineAPI.Extensions;
using ShopOnlineAPI.Repositories.Contracts;
using ShopOnlineModels.Dtos;

namespace ShopOnlineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
        {
            ShoppingCartRepository = shoppingCartRepository;
            ProductRepository = productRepository;
        }

        public IShoppingCartRepository ShoppingCartRepository { get; }
        public IProductRepository ProductRepository { get; }

        [HttpGet]
        [Route("{userId}/GetItems")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(int userId)
        {
            try
            {
                var cartItems = await this.ShoppingCartRepository.GetItems(userId);
                if(cartItems == null)
                {
                    return NoContent();
                }
                var products = await this.ProductRepository.getItems();
                
                if(products == null)
                {
                    throw new Exception("No products exist in the system");
                }
                var cartItemsDto = cartItems.ConvertToDto(products);
                return Ok(cartItemsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartItemDto>> GetItem(int id)
        {
            try
            {
                var cartItem = await this.ShoppingCartRepository.GetItem(id);
                if(cartItem == null)
                {
                    return NotFound();
                }
                var product = await this.ProductRepository.getItem(cartItem.Id);
                if(product == null)
                {
                    throw new Exception("No products exist in the system");
                }
                var cartItemDto = cartItem.ConvertToDto(product);
                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CartItemDto>> PostItem([FromBody] CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var newCartItem = await this.ShoppingCartRepository.AddItem(cartItemToAddDto);
                if(newCartItem == null)
                {
                    return NoContent();
                }
                var product = await ProductRepository.getItem(newCartItem.ProductId);

                if(product == null)
                {
                    throw new Exception($"Something went wrong when attempting to retrieve product(productId:({cartItemToAddDto.ProductId})");
                }
                var newCartItemDto = newCartItem.ConvertToDto(product);
                return CreatedAtAction(nameof(GetItem), new {id=newCartItemDto.Id}, newCartItemDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
