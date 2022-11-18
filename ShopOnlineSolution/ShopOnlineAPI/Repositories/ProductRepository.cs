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

        public Task<IEnumerable<ProductCategory>> getCategory()
        {
            throw new NotImplementedException();
        }

        public Task<Product> getItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> getItems()
        {
            var products = await this._shopOnlineDbContext.Products.ToListAsync();
            return products;
        }
    }
}
