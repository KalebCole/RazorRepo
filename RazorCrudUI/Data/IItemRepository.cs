using RazorRepoUI.Models;

namespace RazorRepoUI.Data
{
    public interface IItemRepository
    {
        Task<IEnumerable<ItemModel>> GetItemsAsync();
    }
}
