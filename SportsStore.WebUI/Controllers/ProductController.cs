using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IBookRepository repository;
        public int PageSize = 4;

        public ProductController(IBookRepository productRepository) => this.repository = productRepository;

        public ViewResult List(Category category, int page = 1)
        {
            BookListViewModel model = new BookListViewModel
            {
                Books = repository.GetBooks()
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                    repository.GetBooks().Count() :
                    repository.GetBooks().Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public FileContentResult GetImage(int ID)
        {
            Book book = repository.GetBookById(ID);
            if(book != null)
            {
                return File(book.CoverImage, book.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}