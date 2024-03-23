using Microsoft.EntityFrameworkCore;
using RazorRepoUI.Models;

namespace RazorRepoUI.Data
{
    public class ItemRepository : IItemRepository
    {
        private readonly ItemsContext _context;

        public ItemRepository(ItemsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemModel>> GetItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }

        
    }
}
