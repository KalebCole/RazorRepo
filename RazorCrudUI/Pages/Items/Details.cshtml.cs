using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorRepoUI.Models;

namespace RazorRepoUI.Pages.Items
{
    public class DetailsModel : PageModel
    {
        private readonly RazorRepoUI.Data.ItemsContext _context;

        public DetailsModel(RazorRepoUI.Data.ItemsContext context)
        {
            _context = context;
        }

        public ItemModel ItemModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // IEnumerable passing a collection of items to make a list
            var itemmodel = await _context.Items.FirstOrDefaultAsync(m => m.Id == id);
            if (itemmodel == null)
            {
                return NotFound();
            }
            else
            {
                ItemModel = itemmodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ItemModel = await _context.Items.FindAsync(id);

            if (ItemModel != null)
            {
                _context.Items.Remove(ItemModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
