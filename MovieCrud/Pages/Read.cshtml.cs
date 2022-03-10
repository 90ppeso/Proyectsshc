using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieCrud.Entity;
using MovieCrud.Models;
using MovieCrud.Paging;

namespace MovieCrud.Pages
{
    public class ReadModel : PageModel
    {
        private readonly IRepository<Movie> repository;
        public ReadModel(IRepository<Movie> repository)
        {
            this.repository = repository;
        }
       // public List<Movie> movieList { get; set; }
        public MovieList movieList { get; set; }
    public async Task OnGet(int id)
        {
            //movieList = await repository.ReadAllAsync();
            movieList = new MovieList();
            int pageSize = 3;
            PagingInfo pagingInfo = new PagingInfo();
            pagingInfo.CurrentPage = id == 0 ? 1 : id;
            pagingInfo.ItemsPerPage = pageSize;
            var skip = pageSize * (Convert.ToInt32(id) - 1);
            var resultTuple = await repository.ReadAllFilterAsync(skip, pageSize);
            pagingInfo.TotalItems = resultTuple.Item2;
            movieList.movies = resultTuple.Item1;
            movieList.pagingInfo = pagingInfo;
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await repository.DeleteAsync(id);
            return RedirectToPage("Read", new { id = 1 });
        }
    }
}
