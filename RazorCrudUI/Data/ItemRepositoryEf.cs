using Microsoft.EntityFrameworkCore;
using RazorRepoUI.Models;

namespace RazorRepoUI.Data
{
    public class ItemRepositoryEf : IItemRepository
    {
        private readonly ItemsContext _context;

        public ItemRepositoryEf(ItemsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemModel>> GetItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<ItemModel> GetItemByID(int id)
        {
            return await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ItemModel>> GetItemsBySearch(string filter)
        {
            return await _context.Items.Where(x => x.Name.Contains(filter)).ToListAsync();
        }

        public Task AddItemAsync(ItemModel item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItemAsync(ItemModel item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemModel>> GetItemsByPriceAsync(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }
    }
}
