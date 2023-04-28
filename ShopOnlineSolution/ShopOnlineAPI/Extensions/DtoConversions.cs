using ShopOnlineAPI.Entitites;
using ShopOnlineModels.Dtos;

namespace ShopOnlineAPI.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<ProductCategoryDto> ConvertToDto(this IEnumerable<ProductCategory> productCategories)
        {
            return(from  productCategory in productCategories
                   select new ProductCategoryDto
                   {
                       Id=productCategory.Id,
                       Name=productCategory.Name,
                       IconCSS=productCategory.IconCSS
                   }).ToList();
        }

        public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products, 
                                                            IEnumerable<ProductCategory> productCategories)
        {
            return (from product in products
                    join productCategory in productCategories
                    on product.CategoryId equals productCategory.Id
                    select new ProductDto
                    {
                        Id= product.Id,
                        Name= product.Name,
                        Description= product.Description,
                        ImageURL= product.ImageURL,
                        Price= product.Price,
                        Quantity= product.Quantity,
                        CategoryId= product.CategoryId,
                        CategoryName = productCategory.Name
                    }).ToList();
        }

        public static ProductDto ConvertToDto(this Product product, ProductCategory productCategory)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = productCategory.Id,
                CategoryName = productCategory.Name
            };
        }

        public static IEnumerable<CartItemDto> ConvertToDto(this IEnumerable<CartItem> cartItems, IEnumerable<Product> products)
        {
            return (from cartItem in cartItems
                    join product in products
                    on cartItem.ProductId equals product.Id
                    select new CartItemDto
                    {
                        Id = cartItem.Id,
                        ProductId = cartItem.ProductId,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        ProductImageURL = product.ImageURL,
                        Price = product.Price,
                        CartId = cartItem.CartId,
                        Quantity=cartItem.Quantity,
                        TotalPrice = product.Price * cartItem.Quantity,
                    }).ToList();
        }
        public static CartItemDto ConvertToDto(this CartItem cartItem, Product product)
        {
            return new CartItemDto
                   {
                        Id = cartItem.Id,
                        ProductId = cartItem.ProductId,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        ProductImageURL = product.ImageURL,
                        Price = product.Price,
                        CartId = cartItem.CartId,
                        Quantity=cartItem.Quantity,
                        TotalPrice = product.Price * cartItem.Quantity,
                   };
        }
    }
}
