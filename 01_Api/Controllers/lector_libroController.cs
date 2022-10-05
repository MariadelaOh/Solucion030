using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using _04_Data;

namespace _01_Api.Controllers
{
    public class lector_libroController : ApiController
    {
        private bookCorner_dbContext db = new bookCorner_dbContext();

        // GET: api/lector_libro
        public IList<lector_libro> Getlector_libro()
        {
            IList<lector_libro> lector_librosTabla = db.lector_libro.ToList();
            IList<lector_libro> lector_libros = new List<lector_libro>();
            foreach (var lector_libroTabla in lector_librosTabla)
            {
                lector_libro lector_Libro = new lector_libro();
                lector_Libro.id = lector_libroTabla.id;
                lector_Libro.resenya = lector_libroTabla.resenya;
                lector_Libro.puntuacion = lector_libroTabla.puntuacion;
                lector_Libro.id_lector = lector_libroTabla.id_lector;
                lector_Libro.id_libro = lector_libroTabla.id_libro;
                

                lector_libros.Add(lector_Libro);
            }
            return lector_libros;
        }

        // GET: api/lector_libro/5
        [ResponseType(typeof(lector_libro))]
        public IHttpActionResult Getlector_libro(int? id, int? siguiente)
        {
            lector_libro lector_Libro = null;
                
            if (siguiente == null)
            {
                lector_Libro= db.lector_libro.Where(x => x.id == id.Value).FirstOrDefault();

                return NotFound();
            }
            else
            {
                if (siguiente.Value == 1)
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


            lector_libro lector_LibroTabla = new lector_libro();

                    
            lector_LibroTabla.id = lector_Libro.id;
            lector_LibroTabla.resenya = lector_Libro.resenya;
            lector_LibroTabla.puntuacion = lector_Libro.puntuacion;
            lector_LibroTabla.id_lector = lector_Libro.id_lector;
            lector_LibroTabla.id_libro = lector_Libro.id_libro;

           
            return Ok(lector_LibroTabla);
            
        }

        // PUT: api/lector_libro/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlector_libro(int id, lector_libro lector_libro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lector_libro.id)
            {
                return BadRequest();
            }

            db.Entry(lector_libro).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!lector_libroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/lector_libro
        [ResponseType(typeof(lector_libro))]
        public IHttpActionResult Postlector_libro(lector_libro lector_libro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.lector_libro.Add(lector_libro);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = lector_libro.id }, lector_libro);
        }

        // DELETE: api/lector_libro/5
        [ResponseType(typeof(lector_libro))]
        public IHttpActionResult Deletelector_libro(int id)
        {
            lector_libro lector_libro = db.lector_libro.Find(id);
            if (lector_libro == null)
            {
                return NotFound();
            }

            db.lector_libro.Remove(lector_libro);
            db.SaveChanges();

            return Ok(lector_libro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool lector_libroExists(int id)
        {
            return db.lector_libro.Count(e => e.id == id) > 0;
        }
    }
}