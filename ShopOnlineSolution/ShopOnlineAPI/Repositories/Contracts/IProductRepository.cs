using ShopOnlineAPI.Entitites;

namespace ShopOnlineAPI.Repositories.Contracts
{
    public interface IProductRepository
    { 
        Task<IEnumerable<Product>> getItems();
        Task<IEnumerable<ProductCategory>> getCategories();
        Task<Product> getItem(int id);
        Task<ProductCategory> getCategory(int id);
        Task<IEnumerable<Product>> getItemsByCategory(int id);
    }
}
