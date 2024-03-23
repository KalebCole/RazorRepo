using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorRepoUI.Models;

namespace RazorRepoUI.Pages.Items
{
    public class CreateModel : PageModel
    {
        private readonly RazorRepoUI.Data.ItemsContext _context;

        // this is handled by the dependency injection system
        // we will not see this being called, but it will be called by the system
        public CreateModel(RazorRepoUI.Data.ItemsContext context)
        {
            _context = context;
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
            ItemModel.CreatedAt = DateTime.UtcNow;
            ItemModel.CreatedBy = Environment.UserName;
            // updating program representation of the database 
            _context.Items.Add(ItemModel);
            // where things get written to the database
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
