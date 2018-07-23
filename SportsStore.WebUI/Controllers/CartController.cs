using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IOrderProcessor orderProcessor;
        private IBookRepository repository;

        public CartController(IBookRepository repo, IOrderProcessor proc) {
            repository = repo;
            orderProcessor = proc; 
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        
        public ActionResult AddToCart(Cart cart, int ID, string returnUrl)
        {
            Book book = repository.GetBookById(ID);

            if (book != null)
                cart.AddItem(book);

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int bookId, string returnUrl)
        {
            Book book = repository.GetBookById(bookId);

            if (book != null)
                cart.RemoveItem(book);

            return RedirectToAction("Index", new { returnUrl });
            
        }

        public PartialViewResult Summary(Cart cart) => PartialView(cart);


        public ViewResult Checkout()
        {
            if(!HttpContext.User.Identity.IsAuthenticated)
            {
                //offer registration
            }
            else if (HttpContext.User.IsInRole("Managers") || HttpContext.User.IsInRole("Administrators"))
            {
                ModelState.AddModelError("", "Only Customers are allowed");
                return View("AuthorizationError");
            }
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.GetBooksInCart.Count() == 0)
                ModelState.AddModelError("", "Sorry, your cart is empty!");

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
                return View(shippingDetails);
        }
        
    }
}