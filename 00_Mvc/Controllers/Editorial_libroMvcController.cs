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
    public class Editorial_libroMvcController : Controller
    {
        private bookCorner_dbContext db = new bookCorner_dbContext();

        // GET: Editorial_libroMvc
        public ActionResult Index(int? id, bool? editorial)
        {
            var editorial_libros = db.editorial_libro.Include(l => l.libro).Include(l => l.editorial);

            if (id != null && id > 0)
            {
                if (editorial != null)
                {
                    if (editorial == true)
                    {
                        editorial_libros = editorial_libros.Where(l => l.id == id);

                        if (editorial_libros != null && editorial_libros.Count() > 0)
                        {
                            ViewBag.Message = "Editorial del Libro: " + editorial_libros.FirstOrDefault().editorial.nombre;
                        }
                    }
                    else
                    {
                        editorial_libros = editorial_libros.Where(x => x.id_libro == id);

                        if (editorial_libros != null && editorial_libros.Count() > 0)
                        {
                            ViewBag.Message = "Editorial del Libro: " + editorial_libros.FirstOrDefault().libro.nombre;
                        }
                    }
                }
            }

            return View(editorial_libros.ToList());
        }


        // GET: Editorial_libroMvc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            editorial_libro editorial_libro = db.editorial_libro.Find(id);
            if (editorial_libro == null)
            {
                return HttpNotFound();
            }
            return View(editorial_libro);
        }

        // GET: Editorial_libroMvc/Create
        public ActionResult Create()
        {
            ViewBag.id_editorial = new SelectList(db.editorial, "id", "nombre");
            ViewBag.id_libro = new SelectList(db.libro, "id", "nombre");
            return View();
        }

        // POST: Editorial_libroMvc/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_editorial,id_libro")] editorial_libro editorial_libro)
        {
            if (ModelState.IsValid)
            {
                db.editorial_libro.Add(editorial_libro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_editorial = new SelectList(db.editorial, "id", "nombre", editorial_libro.id_editorial);
            ViewBag.id_libro = new SelectList(db.libro, "id", "nombre", editorial_libro.id_libro);
            return View(editorial_libro);
        }

        // GET: Editorial_libroMvc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            editorial_libro editorial_libro = db.editorial_libro.Find(id);
            if (editorial_libro == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_editorial = new SelectList(db.editorial, "id", "nombre", editorial_libro.id_editorial);
            ViewBag.id_libro = new SelectList(db.libro, "id", "nombre", editorial_libro.id_libro);
            return View(editorial_libro);
        }

        // POST: Editorial_libroMvc/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_editorial,id_libro")] editorial_libro editorial_libro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(editorial_libro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_editorial = new SelectList(db.editorial, "id", "nombre", editorial_libro.id_editorial);
            ViewBag.id_libro = new SelectList(db.libro, "id", "nombre", editorial_libro.id_libro);
            return View(editorial_libro);
        }

        // GET: Editorial_libroMvc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            editorial_libro editorial_libro = db.editorial_libro.Find(id);
            if (editorial_libro == null)
            {
                return HttpNotFound();
            }
            return View(editorial_libro);
        }

        // POST: Editorial_libroMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            editorial_libro editorial_libro = db.editorial_libro.Find(id);
            db.editorial_libro.Remove(editorial_libro);
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
