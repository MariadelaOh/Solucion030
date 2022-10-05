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
    public class LectorsMvcController : Controller
    {
        private bookCorner_dbContext db = new bookCorner_dbContext();

        // GET: LectorsMvc
        public ActionResult Index()
        {
            return View(db.lector.ToList());
        }

        // GET: LectorsMvc/Details/5
        public ActionResult Details(int? id, bool? siguiente)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lector lectorX = null;
            if (siguiente == null)
            {
                lectorX = db.lector.Where(x => x.id == id.Value).FirstOrDefault();
            }
            else
            {
                if (siguiente.Value == true)
                {
                    lectorX = db.lector.Where(x => x.id > id.Value).FirstOrDefault();
                }
                else
                {
                    IList<lector> lectors = db.lector.Where(x => x.id < id.Value).ToList();
                    if (lectors != null && lectors.Count() > 0)
                    {
                        int? idCategoria = lectors.Max(x => x.id);
                        lectorX = db.lector.Where(x => x.id == idCategoria.Value).FirstOrDefault();
                    }
                }
            }
            if (lectorX == null)
            {
                lectorX = db.lector.Where(x => x.id == id.Value).FirstOrDefault();
            }
            return View(lectorX);
        }

        // GET: LectorsMvc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LectorsMvc/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,apellido,login,password")] lector lector)
        {
            if (ModelState.IsValid)
            {
                db.lector.Add(lector);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lector);
        }

        // GET: LectorsMvc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lector lector = db.lector.Find(id);
            if (lector == null)
            {
                return HttpNotFound();
            }
            return View(lector);
        }

        // POST: LectorsMvc/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,apellido,login,password")] lector lector)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lector).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lector);
        }

        // GET: LectorsMvc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lector lector = db.lector.Find(id);
            if (lector == null)
            {
                return HttpNotFound();
            }
            return View(lector);
        }

        // POST: LectorsMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            lector lector = db.lector.Find(id);
            db.lector.Remove(lector);
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
