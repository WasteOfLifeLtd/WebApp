using System.Collections.Generic;
using System.Linq;
using SportsStore.Domain.Abstract;
using System.Web.Mvc;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private  IBookRepository repository;

        public NavController(IBookRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<Category> categories = repository.GetBooks()
                                .Select(x => x.Category)
                                .Distinct()
                                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}