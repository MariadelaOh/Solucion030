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
    public class Lector_libroMvcController : Controller
    {
        private bookCorner_dbContext db = new bookCorner_dbContext();

        // GET: Lector_libroMvc
        public ActionResult Index(int? id, bool? libro)
        {
           IList<lector_libro> lector_libros = db.lector_libro.Include(l => l.libro).Include(l => l.lector).ToList();

            if (id != null && id > 0)
            {
                if (libro != null)
                {
                    if (libro == true)
                    {
                        lector_libros = lector_libros.Where(l => l.id_libro == id).ToList();

                        if (lector_libros != null && lector_libros.Count() > 0)
                        {
                            ViewBag.Message = "Lector del Libro: " + lector_libros.FirstOrDefault().lector.nombre;
                        }
                    }
                    else
                    {
                        lector_libros = lector_libros.Where(x => x.id_lector == id).ToList();

                        if (lector_libros != null && lector_libros.Count() > 0)
                        {
                            ViewBag.Message = "Lector del Libro: " + lector_libros.FirstOrDefault().libro.nombre;
                        }
                    }
                }
            }

            return View(lector_libros);
        }


        public ActionResult Details(int? id, bool? siguiente)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lector_libro lector_Libro = null;
            if (siguiente == null)
            {
                lector_Libro = db.lector_libro.Where(x => x.id == id.Value).FirstOrDefault();
            }
            else
            {
                if (siguiente.Value == true)
                {
                    lector_Libro = db.lector_libro.Where(x => x.id > id.Value).FirstOrDefault();
                }
                else
                {
                    IList<lector_libro> lector_libros = db.lector_libro.Where(x => x.id < id.Value).ToList();
                    if (lector_libros != null && lector_libros.Count() > 0)
                    {
                        int? idLector_libro = lector_libros.Max(x => x.id);
                        lector_Libro = db.lector_libro.Where(x => x.id == idLector_libro.Value).FirstOrDefault();
                    }
                }
            }
            if (lector_Libro == null)
            {
                lector_Libro = db.lector_libro.Where(x => x.id == id.Value).FirstOrDefault();
            }
            return View(lector_Libro);
        }


        // GET: Lector_libroMvc/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    lector_libro lector_libro = db.lector_libro.Find(id);
        //    if (lector_libro == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lector_libro);
        //}

        // GET: Lector_libroMvc/Create
        public ActionResult Create()
        {
            ViewBag.id_lector = new SelectList(db.lector, "id", "nombre");
            ViewBag.id_libro = new SelectList(db.libro, "id", "nombre");
            return View();
        }

        // POST: Lector_libroMvc/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_lector,id_libro,resenya,puntuacion")] lector_libro lector_libro)
        {
            if (ModelState.IsValid)
            {
                db.lector_libro.Add(lector_libro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_lector = new SelectList(db.lector, "id", "nombre", lector_libro.id_lector);
            ViewBag.id_libro = new SelectList(db.libro, "id", "nombre", lector_libro.id_libro);
            return View(lector_libro);
        }

        // GET: Lector_libroMvc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lector_libro lector_Libro = db.lector_libro.Find(id);
            if (lector_Libro == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_lector = new SelectList(db.lector, "id", "nombre", lector_Libro.id_lector);
            ViewBag.id_libro = new SelectList(db.libro, "id", "nombre", lector_Libro.id_libro);
            return View(lector_Libro);
        }

        // POST: Lector_libroMvc/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_lector,id_libro,resenya,puntuacion")] lector_libro lector_libro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lector_libro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_lector = new SelectList(db.lector, "id", "nombre", lector_libro.id_lector);
            ViewBag.id_libro = new SelectList(db.libro, "id", "nombre", lector_libro.id_libro);
            return View(lector_libro);
        }

        // GET: Lector_libroMvc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lector_libro lector_libro = db.lector_libro.Find(id);
            if (lector_libro == null)
            {
                return HttpNotFound();
            }
            return View(lector_libro);
        }

        // POST: Lector_libroMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            lector_libro lector_libro = db.lector_libro.Find(id);
            db.lector_libro.Remove(lector_libro);
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


        // POST: Ejercicio/_LectorLibroMvcPartialView/5
        [HttpPost]
        public ActionResult _LectorLibroMvcPartialView(lector_libro lector_Libro)
        {
            return View("_LectorLibroMvcPartialView", lector_Libro);
        }



    }
}
