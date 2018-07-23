using SportsStore.Domain.Abstract;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using SportsStore.Domain.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using SportsStore.WebUI.Models;
using System.Text;

namespace SportsStore.WebUI.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {
        private IBookRepository repository;

        public AdminController(IBookRepository repo) => repository = repo;

        // GET: Admin
        public ViewResult Index()
        {
            return View(repository.GetBooks());
        }

        public ViewResult Edit(int bookId)
        {
            Book book = repository.GetBookById(bookId);
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null )
                {
                    book.ImageMimeType = image.ContentType;
                    book.CoverImage= new byte[image.ContentLength];
                    image.InputStream.Read(book.CoverImage, 0, image.ContentLength);
                }
                repository.UpdateBook(book);
                repository.Save();
                TempData["message"] = string.Format($"{book.Title} has been saved");
                return RedirectToAction("Index");
            }
            else
                return View(book);
        }

        //public ViewResult Create()
        //{
        //    return View("Edit", new Product());
        //}

        [HttpPost]
        public ActionResult Delete(int bookId)
        {
            Book deletedBook = repository.DeleteBook(bookId);
            if (deletedBook!= null)
            {
                TempData["message"] = string.Format($"{deletedBook.Title} was deleted");
            }
            return RedirectToAction("Index");
        }
    }
}