using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorRepoUI.Data;
using RazorRepoUI.Models;

namespace RazorRepoUI.Pages.Items
{
    public class EditModel : PageModel
    {
        private readonly IItemRepository _repo;

        public EditModel(IItemRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public ItemModel ItemModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemmodel = await _repo.GetItemByIDAsync(id.Value);
            if (itemmodel == null)
            {
                return NotFound();
            }
            ItemModel = itemmodel;
            return Page(); //has references to the properties of the ItemModel
            // this is the same as return View(ItemModel);
            // View is a method that returns a view result
            // 
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var dbItem = await _repo.UpdateItemAsync(ItemModel);
            if (dbItem == false)
            {
                ModelState.AddModelError(string.Empty, "Failed to update item.");
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
