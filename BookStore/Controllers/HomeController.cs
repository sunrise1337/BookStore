using BookStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext context = new ApplicationDbContext();

        

        public ActionResult Admin()
        {

            return View(context.Users.ToList());
        }

        public ActionResult GetPurchased(string uname)
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                                    .GetUserManager<ApplicationUserManager>();

            ApplicationUser user = userManager.FindByName(uname);
            ViewBag.Name = user.UserName + " purchased books";

            return View("Purchased", user.Purchased.ToList());
        }


        //public ActionResult Create()
        //{

        //    return View();
        //}

        //// POST: Authors/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(ApplicationUser user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        context.Users.Add(user);
        //        context.SaveChanges();
        //        return RedirectToAction("Admin");
        //    }

        //    //ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName", book.AuthorId);
        //    //ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", book.GenreId);
        //    return View(user);
        //}




        //public ActionResult Delete(string id)
        //{
        //    ApplicationUser user = context.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        //// POST: Books/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    ApplicationUser user = context.Users.Find(id);
        //    context.Users.Remove(user);
        //    context.SaveChanges();
        //    return RedirectToAction("Admin");
        //}


        public ActionResult GetWishlist(string uname)
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                                    .GetUserManager<ApplicationUserManager>();

            ApplicationUser user = userManager.FindByName(uname);
            ViewBag.Name = user.UserName + " wished books";

            return View("Wishlist", user.Wishlist.ToList());
        }

        public ActionResult MinusKarma(string uname)
        {
            var u = new ApplicationUser();

            foreach (var user in context.Users)
            {
                if (user.UserName == uname)
                {
                    user.Karma -= 5;
                    if (user.Karma <= 0 && user.Roles.Count >= 2)
                        user.isBanned = true;


                    u = user;

                }
            }

            context.SaveChanges();
            return PartialView("MinusKarma", u);
        }

        public ActionResult PlusKarma(string uname)
        {
            var u = new ApplicationUser();

            foreach (var user in context.Users)
            {
                if (user.UserName == uname)
                {
                    user.Karma += 5;
                    u = user;
                }
            }

            context.SaveChanges();
            return PartialView("MinusKarma", u);
        }

        


    }
}