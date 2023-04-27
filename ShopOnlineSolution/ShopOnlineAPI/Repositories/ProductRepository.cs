using Microsoft.EntityFrameworkCore;
using ShopOnlineAPI.Data;
using ShopOnlineAPI.Entitites;
using ShopOnlineAPI.Repositories.Contracts;

namespace ShopOnlineAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopOnlineDbContext _shopOnlineDbContext;
        public ProductRepository(ShopOnlineDbContext shopOnlineDbContext)
        {
            this._shopOnlineDbContext = shopOnlineDbContext;
        }

        public async Task<IEnumerable<ProductCategory>> getCategories()
        {
            var categories = await this._shopOnlineDbContext.ProductCategories.ToListAsync();
            return categories;
        }

        public async Task<ProductCategory> getCategory(int id)
        {
            var category = await _shopOnlineDbContext.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<Product> getItem(int id)
        {
            var product = await _shopOnlineDbContext.Products.FindAsync(id);
            return product;
        }

        public async Task<IEnumerable<Product>> getItems()
        {
            var products = await this._shopOnlineDbContext.Products.ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> getItemsByCategory(int id)
        {
            var products = await (from product in this._shopOnlineDbContext.Products
                                  where product.CategoryId == id
                                  select product).ToListAsync();
            return products;
        }
    }
}
