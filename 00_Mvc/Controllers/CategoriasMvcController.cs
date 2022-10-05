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
    public class CategoriasMvcController : Controller
    {
        private bookCorner_dbContext db = new bookCorner_dbContext();

        // GET: CategoriasMvc
        public ActionResult Index()
        {
            return View(db.categoria.ToList());
        }

        // GET: CategoriasMvc/Details/5
        public ActionResult Details(int? id, bool? siguiente)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categoria categoriaX = null;
            if (siguiente == null)
            {
                categoriaX = db.categoria.Where(x => x.id == id.Value).FirstOrDefault();
            }
            else
            {
                if (siguiente.Value == true)
                {
                    categoriaX = db.categoria.Where(x => x.id > id.Value).FirstOrDefault();
                }
                else
                {
                    IList<categoria> categorias = db.categoria.Where(x => x.id < id.Value).ToList();
                    if (categorias != null && categorias.Count() > 0)
                    {
                        int? idCategoria = categorias.Max(x => x.id);
                        categoriaX = db.categoria.Where(x => x.id == idCategoria.Value).FirstOrDefault();
                    }
                }
            }
            if (categoriaX == null)
            {
                categoriaX = db.categoria.Where(x => x.id == id.Value).FirstOrDefault();
            }
            return View(categoriaX);
        }

        // GET: CategoriasMvc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriasMvc/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre")] categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.categoria.Add(categoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: CategoriasMvc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categoria categoria = db.categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: CategoriasMvc/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre")] categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: CategoriasMvc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categoria categoria = db.categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: CategoriasMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            categoria categoria = db.categoria.Find(id);
            db.categoria.Remove(categoria);
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
