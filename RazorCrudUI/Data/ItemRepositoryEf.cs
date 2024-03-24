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

        public async Task<ItemModel> GetItemByID(int? id)
        {
            return await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ItemModel>> GetItemsBySearch(string filter)
        {
            return await _context.Items.Where(x => x.Name.Contains(filter)).ToListAsync();
        }

        public async Task InsertItemAsync(ItemModel item)
        {
            item.CreatedAt = DateTime.UtcNow;
            item.CreatedBy = Environment.UserName;
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        public Task UpdateItemAsync(ItemModel item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteItemAsync(int id)
        {
            var itemModel =  await _context.Items.FindAsync(id);
            if (itemModel != null)
            {
                itemModel.isDeleted = true;
                _context.Items.Update(itemModel);
                await _context.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<ItemModel>> GetItemsByPriceAsync(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }
    }
}
