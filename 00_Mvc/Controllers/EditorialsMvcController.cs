using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _04_Data;

namespace _00_Mvc.Controllers
{
    public class EditorialsMvcController : Controller
    {
        private bookCorner_dbContext db = new bookCorner_dbContext();

        // GET: EditorialsMvc
        public ActionResult Index()
        {
            return View(db.editorial.ToList());
        }

        // GET: EditorialsMvc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            editorial editorial = db.editorial.Find(id);
            if (editorial == null)
            {
                return HttpNotFound();
            }
            return View(editorial);
        }

        // GET: EditorialsMvc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EditorialsMvc/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre")] editorial editorial)
        {
            if (ModelState.IsValid)
            {
                db.editorial.Add(editorial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(editorial);
        }

        // GET: EditorialsMvc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            editorial editorial = db.editorial.Find(id);
            if (editorial == null)
            {
                return HttpNotFound();
            }
            return View(editorial);
        }

        // POST: EditorialsMvc/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre")] editorial editorial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(editorial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editorial);
        }

        // GET: EditorialsMvc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            editorial editorial = db.editorial.Find(id);
            if (editorial == null)
            {
                return HttpNotFound();
            }
            return View(editorial);
        }

        // POST: EditorialsMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            editorial editorial = db.editorial.Find(id);
            db.editorial.Remove(editorial);
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
