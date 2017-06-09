using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BookStore.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BookStore.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Index
        // GET: Books
        public ActionResult Index(string searchName, string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price desc" : "Price";
            ViewBag.GenreSortParm = sortOrder == "Genre" ? "Genre desc" : "Genre";
            ViewBag.AuthorSortParm = sortOrder == "Author" ? "Author desc" : "Author";
            ViewBag.RateSortParm = sortOrder == "Rate" ? "Rate desc" : "Rate";


            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            ViewBag.WishList = db.Users.Find(user.Id).Wishlist.ToList();


            var books = db.Books.Include(b => b.Author).Include(b => b.Genre);

            ViewBag.TopBooks = books.OrderByDescending(i => i.Rate).Take(5).DistinctBy(p => new { p.Name, p.Author.FirstName, p.Author.LastName }).ToList();
            ViewBag.TopAuthors = db.Authors.OrderByDescending(i => i.Rate).Take(5).DistinctBy(p => new { p.FirstName, p.LastName }).ToList();

            switch (sortOrder)
            {
                case "Name desc":
                    books = books.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    books = books.OrderBy(s => s.Price);
                    break;
                case "Price desc":
                    books = books.OrderByDescending(s => s.Price);
                    break;
                case "Genre":
                    books = books.OrderBy(s => s.Genre.Name);
                    break;
                case "Genre desc":
                    books = books.OrderByDescending(s => s.Genre.Name);
                    break;
                case "Author":
                    books = books.OrderBy(s => s.Author.FirstName);
                    break;
                case "Author desc":
                    books = books.OrderByDescending(s => s.Author.FirstName);
                    break;
                case "Rate":
                    books = books.OrderBy(s => s.Rate);
                    break;
                case "Rate desc":
                    books = books.OrderByDescending(s => s.Rate);
                    break;
                default:
                    books = books.OrderBy(s => s.Name);
                    break;
            }


            if (!string.IsNullOrEmpty(searchName))
            {
                //Search
                books = books.Where(x => x.Name.Contains(searchName) || x.Author.FirstName.Contains(searchName) 
                || x.Author.LastName.Contains(searchName) || x.Genre.Name.Contains(searchName));
            }

            return View(books.ToList());
        }

        #endregion


        #region Details

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        #endregion


        #region Create

        //// GET: Books/Create
        //public ActionResult Create()
        //{
        //    ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName");
        //    ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name");
        //    return View();
        //}

        //// POST: Books/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,ImagePath,GenreId,AuthorId,Rate,Price,Amount")] Book book)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Books.Add(book);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName", book.AuthorId);
        //    ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", book.GenreId);
        //    return View(book);
        //}

        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "LastName");
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name");

            string[] filePaths = Directory.GetFiles(Server.MapPath("~/BookImages/"));
            List<string> files = new List<string>();
            foreach (string filePath in filePaths)
            {
                files.Add("~/BookImages/" + Path.GetFileName(filePath));
            }
            ViewBag.Files = files;

            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "LastName", book.AuthorId);
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", book.GenreId);
            return View(book);
        }

        #endregion


        #region Edit

        //// GET: Books/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Book book = db.Books.Find(id);
        //    if (book == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName", book.AuthorId);
        //    ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", book.GenreId);
        //    return View(book);
        //}

        //// POST: Books/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,ImagePath,GenreId,AuthorId,Rate,Price,Amount")] Book book)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(book).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName", book.AuthorId);
        //    ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", book.GenreId);
        //    return View(book);
        //}

        // GET: Books/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "LastName", book.AuthorId);
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", book.GenreId);

            string[] filePaths = Directory.GetFiles(Server.MapPath("~/BookImages/"));
            List<string> files = new List<string>();
            foreach (string filePath in filePaths)
            {
                files.Add("~/BookImages/" + Path.GetFileName(filePath));
            }
            ViewBag.Files = files;

            return View(book);
        }

        // POST: Books/Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "LastName", book.AuthorId);
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", book.GenreId);
            return View(book);
        }

        #endregion


        #region BuyBook

        [Authorize]
        public ActionResult BuyBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            db.Books.Find(id).Amount -= 1;

            db.Books.Find(id).Rate += 1;
            db.Books.Find(id).Author.Rate += 1;


            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            db.Users.Find(user.Id).Purchased.Add(book);
            db.SaveChanges();

            return View(book);
        }

        [Authorize]
        public ActionResult WishBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            db.Users.Find(user.Id).Wishlist.Add(book);
            db.SaveChanges();

            //Content("<script language='javascript' type='text/javascript'>alert('This book in your WishList!');</script>");
            return RedirectToAction("Index");
        }


        #endregion


        #region Delete

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion


        #region Create Author

        public ActionResult CreateAuthor()
        {
            //ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName");
            //ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name");

            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName", book.AuthorId);
            //ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", book.GenreId);
            return View(author);
        }

        #endregion


        #region Create Genre

        public ActionResult CreateGenre()
        {
            return View();
        }

        // POST: Genres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGenre(Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(genre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(genre);
        }

        #endregion


        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

    }
}
