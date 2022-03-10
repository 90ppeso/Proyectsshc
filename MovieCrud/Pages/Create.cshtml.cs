using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieCrud.Entity;
using MovieCrud.Models;

namespace MovieCrud.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IRepository<Movie> repository;
        public CreateModel(IRepository<Movie> repository)
        {
            this.repository = repository;
        }

        [BindProperty]
        public Movie movie { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await repository.CreateAsync(movie);
                return RedirectToPage("Read", new { id = 1 });
            }
            else                
                return Page();
        }

    }
}
