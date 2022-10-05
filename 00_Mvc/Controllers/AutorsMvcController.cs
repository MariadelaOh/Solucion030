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
    public class AutorsMvcController : Controller
    {
        private bookCorner_dbContext db = new bookCorner_dbContext();

        // GET: AutorsMvc
        public ActionResult Index()
        {
            return View(db.autor.ToList());
        }

        // GET: AutorsMvc/Details/5

        public ActionResult Details(int? id, bool? siguiente)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            autor autore = null;
            if (siguiente == null)
            {
                autore = db.autor.Where(x => x.id == id.Value).FirstOrDefault();
            }
            else
            {
                if (siguiente.Value == true)
                {
                    autore = db.autor.Where(x => x.id > id.Value).FirstOrDefault();
                }
                else
                {
                    IList<autor> autors = db.autor.Where(x => x.id < id.Value).ToList();
                    if (autors != null && autors.Count() > 0)
                    {
                        int? idAutor = autors.Max(x => x.id);
                        autore = db.autor.Where(x => x.id == idAutor.Value).FirstOrDefault();
                    }
                }
            }
            if (autore == null)
            {
                autore = db.autor.Where(x => x.id == id.Value).FirstOrDefault();
            }
            return View(autore);
        }

        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    autor autor = db.autor.Find(id);
        //    if (autor == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(autor);
        //}

        // GET: AutorsMvc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutorsMvc/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,apellido,imagen")] autor autor)
        {
            if (ModelState.IsValid)
            {
                db.autor.Add(autor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(autor);
        }

        // GET: AutorsMvc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            autor autor = db.autor.Find(id);
            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        // POST: AutorsMvc/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,apellido,imagen")] autor autor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autor);
        }

        // GET: AutorsMvc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            autor autor = db.autor.Find(id);
            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        // POST: AutorsMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            autor autor = db.autor.Find(id);
            db.autor.Remove(autor);
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



        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult _EmpleadoMvcPartialView(Empleado empleado)
        //{
        //    return View("_EmpleadoMvcPartialView", empleado);
        //}


    }
}
