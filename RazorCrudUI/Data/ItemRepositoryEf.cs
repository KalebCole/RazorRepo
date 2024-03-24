using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
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
            return await _context.Items.Where(x => x.Name.Contains(filter) && x.isDeleted == false).ToListAsync();
        }

        public async Task InsertItemAsync(ItemModel item)
        {
            item.CreatedAt = DateTime.UtcNow;
            item.CreatedBy = Environment.UserName;
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateItemAsync(ItemModel item)
        {
            var dbItem = await GetItemByID(item.Id);
            if (dbItem == null)
            {
                return false;
            }

            if (dbItem.isDeleted)
            {
                return false;
            }

            if (!(item.Name.Equals(dbItem.Name) && item.Description.Equals(dbItem.Description) && item.Price.Equals(dbItem.Price)))
            {
                if (!item.Name.Equals(dbItem.Name))
                {
                    dbItem.Name = item.Name;
                }
                if (!item.Description.Equals(dbItem.Description))
                {
                    dbItem.Description = item.Description;
                }
                if (item.Price != dbItem.Price)
                {
                    dbItem.Price = item.Price;
                }
            }
            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                // Handle the concurrency exception
                var errorMessage = "The item you are trying to update has been modified by another user or process. Please refresh the page and try again.";
                // Return the error message to the caller
                throw new Exception(errorMessage);

            }
            return true;
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
            try
            {
                await _context.SaveChangesAsync();
            }
            // move this to the repository and make it a boolean return
            catch (DbUpdateConcurrencyException)
            {
                // Handle the concurrency exception
                var errorMessage = "The item you are trying to update has been modified by another user or process. Please refresh the page and try again.";
                // Return the error message to the caller
                throw new Exception(errorMessage);
            }
        }

        public Task<IEnumerable<ItemModel>> GetItemsByPriceAsync(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }
    }
}
