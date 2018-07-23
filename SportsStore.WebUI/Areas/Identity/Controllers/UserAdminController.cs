using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Entities.IdentityEntities;
using SportsStore.WebUI.Areas.Identity.Models;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Areas.Identity.Controllers
{
    [Authorize(Roles = "Administrators,Managers")]
    public class UserAdminController : Controller
    {
        private AppUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }

        private AppRoleManager RoleManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>(); }
        }

        public ActionResult Users()
        {
            if (HttpContext.User.IsInRole("Administrators"))
            {
                return View(UserManager.Users);
            }
            else
            {
                AppRole customerRole = RoleManager.FindByName("Customers");
                string[] customerIds = customerRole.Users.Select(x => x.UserId).ToArray();
                return View(UserManager.Users.Where(user => customerIds.Any(id => id == user.Id)).ToList());
            }
        }

        [Authorize(Roles = "Administrators")]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        public async Task<ActionResult> CreateUser(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.Username,
                                            Email = model.Email,
                                            FirstName = model.FirstName,
                                            LastName = model.LastName,
                                            AddressLine1 = model.AddressLine1,
                                            AddressLine2 = model.AddressLine2,
                                            AddressLine3 = model.AddressLine3 };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Users");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(error);
                TempData["message"] = builder.ToString();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData["message"] = string.Format($"{user.UserName} has been deleted");
                    return RedirectToAction("Users");
                }
                else
                {
                    TempData["message"] = string.Format($"couldn't delete {user.UserName}");
                    return View("Users");
                }
            }
            else
            {
                TempData["message"] = string.Format($"User not found");
                return View("Users");
            }
        }

        public async Task<ActionResult> EditUser(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Users");
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(string id, string email, string password)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                IdentityResult validEmail = await UserManager.UserValidator.ValidateAsync(user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPass = null;
                if (password != string.Empty)
                {
                    validPass = await UserManager.PasswordValidator.ValidateAsync(password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Users");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                TempData["message"] = string.Format($"User not found");
                return View("Users");
            }
            return View(user);
        }
    }
}