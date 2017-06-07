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

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Index
        // GET: Books
        public ActionResult Index(string searchName, string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price desc" : "Price";

            var books = db.Books.Include(b => b.Author).Include(b => b.Genre);

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
                default:
                    books = books.OrderBy(s => s.Name);
                    break;
            }


            if (!string.IsNullOrEmpty(searchName))
            {
                //Search
                books = books.Where(x => x.Name.Contains(searchName) || x.Author.FirstName.Contains(searchName) 
                || x.Author.LastName.Contains(searchName) || x.Genre.Name.Contains(searchName));
                //return View(books.ToList());
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
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName");
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

            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName", book.AuthorId);
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
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName", book.AuthorId);
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
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName", book.AuthorId);
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", book.GenreId);
            return View(book);
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
