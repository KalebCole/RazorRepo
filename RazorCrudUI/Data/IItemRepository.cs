using RazorRepoUI.Models;

namespace RazorRepoUI.Data
{
    public interface IItemRepository
    {
        Task<IEnumerable<ItemModel>> GetItemsAsync();
        Task<ItemModel> GetItemByID(int id);
        Task<IEnumerable<ItemModel>> GetItemsBySearch(string filter);

        Task InsertItemAsync(ItemModel item);
        Task UpdateItemAsync(ItemModel item);

        Task DeleteItemAsync(int id);

        Task<IEnumerable<ItemModel>> GetItemsByPriceAsync(decimal minPrice, decimal maxPrice);

    }
}
