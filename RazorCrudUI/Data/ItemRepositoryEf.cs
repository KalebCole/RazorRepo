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
            return await _context.Items.Where(x => x.isDeleted == false).ToListAsync();
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

            await _context.SaveChangesAsync(); //things that have a changed state will be updated



            return true;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var itemModel = await GetItemByID(id);
            if (itemModel == null)
            {
                return false;
            }
            //find the item in the db and see if the item model matches the item model in the db
            var dbItem = await GetItemByID(id);
            if (!itemModel.Equals(dbItem))
            {
                return false;
            }

            itemModel.isDeleted = true;
            _context.Items.Update(itemModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<ItemModel>> GetItemsByPriceAsync(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }
    }
}
