using CRUD_Alumnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Alumnos.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index()
        {
            try
            {
                using (AlumnosEntities db = new AlumnosEntities())
                {
                    List<Alumno> listAlum = db.Alumno.ToList();
                    return View(listAlum);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Alumno a)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                using (AlumnosEntities db = new AlumnosEntities())
                {
                    a.FechaRegistro = DateTime.Now;
                    db.Alumno.Add(a);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Error al cargar alumno " + ex.Message);
                return View();
            }

        }
        public ActionResult Editar(int id)
        {
            using (AlumnosEntities db = new AlumnosEntities())
            {
                Alumno alu = db.Alumno.Find(id);
                return View(alu);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Alumno a)
        {
            try
            {
                using (AlumnosEntities db = new AlumnosEntities())
                {
                    Alumno alu = db.Alumno.Find(a.Id);
                    alu.Nombres = a.Nombres;
                    alu.Apellido = a.Apellido;
                    alu.Edad = a.Edad;
                    alu.Sexo = a.Sexo;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Detalles(int id)
        {
            using (AlumnosEntities db = new AlumnosEntities())
            {
                try
                {
                    Alumno alu = db.Alumno.Find(id);
                    return View(alu);
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }
        public ActionResult Eliminar(int id)
        {
            try
            {
                using (AlumnosEntities db = new AlumnosEntities())
                {
                    Alumno alu = db.Alumno.Find(id);
                    db.Alumno.Remove(alu);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult ListaCiudades() 
        {
            using (AlumnosEntities db = new AlumnosEntities())
            {
                return PartialView(db.Ciudad.ToList());
            }
        }
        public static string NombreCiudad(int IdCiudad)
        {
            using (AlumnosEntities db = new AlumnosEntities())
            {
                return db.Ciudad.Find(IdCiudad).Nombre;
            }
        }
    }
}