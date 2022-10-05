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
    public class LibrosMvcController : Controller
    {
        private bookCorner_dbContext db = new bookCorner_dbContext();

        // GET: LibrosMvc
        public ActionResult Index(int? id, bool? categoria)
        {
            IList<libro> libros = 
                db.libro.Include(l => l.autor).Include(l => l.categoria).ToList();

            if (id !=  null && id > 0)
            {
                if (categoria != null)
                {
                    if (categoria == true)
                    {
                        libros= libros.Where(l => l.id_categoria == id).ToList();

                        if (libros != null && libros.Count() > 0)
                        {
                            ViewBag.Message = "Categoria de Libros: " + libros.FirstOrDefault().categoria.nombre;
                        }
                    }
                    else
                    {
                        libros= libros.Where(x=>x.id_autor == id).ToList();

                        if (libros != null && libros.Count() > 0)
                        {
                            ViewBag.Message = "Libros del Autor: " + libros.FirstOrDefault().autor.nombre;
                        }
                    }
                }
            }
            
            return View(libros);
        }

        // GET: LibrosMvc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            libro libro = db.libro.Find(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        // GET: LibrosMvc/Create
        public ActionResult Create()
        {
            ViewBag.id_autor = new SelectList(db.autor, "id", "nombre");
            ViewBag.id_categoria = new SelectList(db.categoria, "id", "nombre");
            return View();
        }

        // POST: LibrosMvc/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_categoria,id_autor,nombre,imagen,resumen")] libro libro)
        {
            if (ModelState.IsValid)
            {
                db.libro.Add(libro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_autor = new SelectList(db.autor, "id", "nombre", libro.id_autor);
            ViewBag.id_categoria = new SelectList(db.categoria, "id", "nombre", libro.id_categoria);
            return View(libro);
        }

        // GET: LibrosMvc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            libro libro = db.libro.Find(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_autor = new SelectList(db.autor, "id", "nombre", libro.id_autor);
            ViewBag.id_categoria = new SelectList(db.categoria, "id", "nombre", libro.id_categoria);
            return View(libro);
        }

        // POST: LibrosMvc/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_categoria,id_autor,nombre,imagen,resumen")] libro libro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(libro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_autor = new SelectList(db.autor, "id", "nombre", libro.id_autor);
            ViewBag.id_categoria = new SelectList(db.categoria, "id", "nombre", libro.id_categoria);
            return View(libro);
        }

        // GET: LibrosMvc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            libro libro = db.libro.Find(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        // POST: LibrosMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            libro libro = db.libro.Find(id);
            db.libro.Remove(libro);
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
