using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.Data;
using LibraryManagement.Models;
using Newtonsoft.Json;

namespace LibraryManagement
{
    public class BooksController : Controller
    {
        private LibraryMgmtContext db = new LibraryMgmtContext();

        // GET: Books
        public ActionResult Index()
        {
            return View(db.books.ToList());
        }

        public ActionResult Search(string searchtitle, string author)
        {
            var books = db.books.ToList();
         
            if (!string.IsNullOrEmpty(searchtitle))
            {
              books =  books.Where(x => x.Title.Contains(searchtitle)).ToList();
            }
            if (!string.IsNullOrEmpty(author))
            {
                books = books.Where(x => x.Author.Equals(author)).ToList();
            }

            return View(books);
        }

        public ActionResult IssueBook()
        {
            return View();

        }
        public JsonResult SearchBook(int bookid)
        {
            var book = db.books.Where(x => x.Id == bookid && x.count>0).FirstOrDefault();
           
            if (book!=null)
            {
              var  result = JsonConvert.SerializeObject(book, Formatting.Indented,
               new JsonSerializerSettings
               {
                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore
               });
               return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = JsonConvert.SerializeObject(null, Formatting.Indented,
                 new JsonSerializerSettings
                 {
                     ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                 });
                return Json(result, JsonRequestBehavior.AllowGet);

            }


        }

    
        public int GenerateRandomNo()
        {
            int _min = 0;
            int _max = 999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        public JsonResult IssueBookToUser(List<IssuedBooks> issuedBooks)
        {
            int issueid = GenerateRandomNo();  
            foreach(var item in issuedBooks)
            {
                IssuedBooks issueBook = new IssuedBooks();
                issueBook.IssuedTo = item.IssuedTo;
                issueBook.IssuedDate = item.IssuedDate;
                issueBook.BookId = item.BookId;
                issueBook.Period = item.Period;
                issueBook.IsReturned = false;
                var book = db.books.Where(x => x.Id == item.BookId).FirstOrDefault();
                book.count = book.count - 1;

                //update Book table
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();

                issueBook.books = book;

                Random rnd = new Random();
                issueBook.IssueId = issueid;

                //save IssueBook table
                db.issuedBooks.Add(issueBook);
                db.SaveChanges();
            }
            return Json(issueid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReturnBook()
        {
            return View();
        }

        public JsonResult ReturnBookByUser(int BookId,int iId)
        {
            var result = "";
            var issuedBook = db.issuedBooks.Where(x=>x.BookId == BookId&&x.IssueId==iId &&x.IsReturned==false).FirstOrDefault();
            if (issuedBook != null)
            {
                var book = db.books.Where(x => x.Id == BookId).FirstOrDefault();
                book.count = book.count + 1;
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();

                issuedBook.IsReturned = true;
                db.Entry(issuedBook).State = EntityState.Modified;
                db.SaveChanges();

                result = "Book returned successfully!!!";
            }
            else
            {
                result = "Issue Id & Book Id doesn't exists!!!";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Author,PublicationDate,Edition,Price,count")] Books books)
        {
            if (ModelState.IsValid)
            {
                db.books.Add(books);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(books);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Author,PublicationDate,Edition,Price,count")] Books books)
        {
            if (ModelState.IsValid)
            {
                db.Entry(books).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(books);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Books books = db.books.Find(id);
            db.books.Remove(books);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
