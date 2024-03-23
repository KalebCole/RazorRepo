using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorRepoUI.Models;

namespace RazorRepoUI.Pages.Items
{
    public class DeleteModel : PageModel
    {
        private readonly RazorRepoUI.Data.ItemsContext _context;

        public DeleteModel(RazorRepoUI.Data.ItemsContext context)
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

            var itemModel = await _context.Items.FindAsync(id);
            if (itemModel != null)
            {
                itemModel.isDeleted = true;
                _context.Items.Update(itemModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
