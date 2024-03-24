using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorRepoUI.Data;
using RazorRepoUI.Models;

namespace RazorRepoUI.Pages.Items
{
    public class CreateModel : PageModel
    {
        private readonly IItemRepository _repo;

        public CreateModel(IItemRepository repo)
        {
            _repo = repo;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        // we use bind property when we want to update things
        [BindProperty]
        public ItemModel ItemModel { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _repo.InsertItemAsync(ItemModel);
            return RedirectToPage("./Index");
        }
    }
}
