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
    public class autorsController : ApiController
    {
        private bookCorner_dbContext db = new bookCorner_dbContext();

        // GET: api/autors
        public IList<autor> Getautor()
        {
            IList<autor> autorsTabla = db.autor.ToList();
            IList<autor> autors = new List<autor>();
            foreach (var autorTabla in autorsTabla)
            {
                autor autore = new autor();
                autore.id = autorTabla.id;
                autore.nombre = autorTabla.nombre;
                autore.apellido = autorTabla.apellido;
                autore.imagen = autorTabla.imagen;
                autore.libro = autorTabla.libro;
                

                autors.Add(autore);
            }
            return autors;
        }

        // GET: api/Empleados/5
        [ResponseType(typeof(autor))]
        public IHttpActionResult Getautor(int? id, int? siguiente)
        {
            autor autore = null;

            if (siguiente == null)
            {
                autore = db.autor.Where(x => x.id == id.Value).FirstOrDefault();
            }
            else
            {
                if (siguiente.Value == 1)
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


            autor autorTabla = new autor();



            autorTabla.id = autore.id;
            autorTabla.nombre = autore.nombre;
            autorTabla.apellido = autore.apellido;
            autorTabla.imagen = autore.imagen;
            autorTabla.libro = autore.libro;
           


            return Ok(autorTabla);


        
        }

       

        // PUT: api/autors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putautor(int id, autor autore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != autore.id)
            {
                return BadRequest();
            }

            db.Entry(autore).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!autorExists(id))
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

        // POST: api/autors
        [ResponseType(typeof(autor))]
        public IHttpActionResult Postautor(autor autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.autor.Add(autor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = autor.id }, autor);
        }

        // DELETE: api/autors/5
        [ResponseType(typeof(autor))]
        public IHttpActionResult Deleteautor(int id)
        {
            autor autor = db.autor.Find(id);
            if (autor == null)
            {
                return NotFound();
            }

            db.autor.Remove(autor);
            db.SaveChanges();

            return Ok(autor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool autorExists(int id)
        {
            return db.autor.Count(e => e.id == id) > 0;
        }
    }
}