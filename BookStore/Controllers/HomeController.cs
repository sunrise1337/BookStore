using BookStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult Index()
        {
            IList<string> roles = new List<string> { "Роль не определена" };
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                                    .GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);
            return View(roles);
        }

        public ActionResult Admin()
        {

            return View(context.Users.ToList());
        }

        public async System.Threading.Tasks.Task<ActionResult> MinusKarma(string uname)
        {
            var u = new ApplicationUser();

            foreach (var user in context.Users)
            {
                if (user.UserName == uname)
                {
                    user.Karma -= 5;
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}