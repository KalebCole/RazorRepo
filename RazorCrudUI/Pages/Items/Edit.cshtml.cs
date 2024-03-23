using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorRepoUI.Models;

namespace RazorRepoUI.Pages.Items
{
    public class EditModel : PageModel
    {
        private readonly RazorRepoUI.Data.ItemsContext _context;

        public EditModel(RazorRepoUI.Data.ItemsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ItemModel ItemModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemmodel = await _context.Items.FirstOrDefaultAsync(m => m.Id == id);
            if (itemmodel == null)
            {
                return NotFound();
            }
            ItemModel = itemmodel;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var dbItem = await _context.Items.FirstOrDefaultAsync(item => item.Id == ItemModel.Id);
            if (dbItem == null)
            {
                return NotFound();
            }

            if (!(ItemModel.Name.Equals(dbItem.Name) && ItemModel.Description.Equals(dbItem.Description) && ItemModel.Price.Equals(dbItem.Price)))
            {
                if (!ItemModel.Name.Equals(dbItem.Name))
                {
                    dbItem.Name = ItemModel.Name;
                }
                if (!ItemModel.Description.Equals(dbItem.Description))
                {
                    dbItem.Description = ItemModel.Description;
                }
                if (ItemModel.Price != dbItem.Price)
                {
                    dbItem.Price = ItemModel.Price;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            // move this to the repository and make it a boolean return
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemModelExists(ItemModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ItemModelExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
