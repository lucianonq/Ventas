using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ventas.Models;
using Ventas.ViewModels;

namespace Ventas.Controllers
{
    public class EmpleadosController : Controller
    {
        VentasDbContext _context = new VentasDbContext();

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Lista()
        {
            var lista = _context.Empleados.ToList();

            return View(lista);
        }

        public ActionResult Nuevo()
        {
            var empleadoVacio = new EmpleadoViewModel();

            return View("Empleado", empleadoVacio);
        }

        public ActionResult Actualizar(int id)
        {
            var empleado = _context.Empleados.FirstOrDefault(x => x.Id == id);

            if (empleado == null)
                return null;

            var empleadoVM = new EmpleadoViewModel
            {
                Id = empleado.Id,
                Apellido = empleado.Apellido,
                Nombre = empleado.Nombre,
                DNI = empleado.DNI,
                FecNac = empleado.FecNac,
                Legajo = empleado.Legajo
            };

            return View("Empleado", empleadoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guardar(EmpleadoViewModel empleadoVM)
        {
            if (!ModelState.IsValid)
            {
                if (empleadoVM.Id == null)
                    return RedirectToAction("Nuevo");
                else
                    return RedirectToAction("Actualizar", new { id = empleadoVM.Id });
            }
                

            if (empleadoVM.Id == null)
            {
                var empleado = new Empleados()
                {
                    Apellido = empleadoVM.Apellido,
                    Nombre = empleadoVM.Nombre,
                    DNI = empleadoVM.DNI,
                    FecNac = empleadoVM.FecNac,
                    Legajo = empleadoVM.Legajo
                };

                _context.Empleados.Add(empleado);
            }
            else
            {
                var empleadoEnDb = _context.Empleados.FirstOrDefault(x => x.Id == empleadoVM.Id);
                empleadoEnDb.Nombre = empleadoVM.Nombre;
                empleadoEnDb.Apellido = empleadoVM.Apellido;
                empleadoEnDb.DNI = empleadoVM.DNI;
                empleadoEnDb.FecNac = empleadoVM.FecNac;
            }

            _context.SaveChanges();

            return RedirectToAction("Lista");
        }

        public ActionResult Borrar(int id)
        {
            var empleado = _context.Empleados.FirstOrDefault(x => x.Id == id);

            if (empleado == null)
                return RedirectToAction("Lista");

            try
            {
                _context.Empleados.Remove(empleado);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                ViewBag.Error = "Empleado ya asociado a una venta. Elimine la factura para proceder.";
            }

            var lista = _context.Empleados.ToList();
            return View("Lista", lista);
        }
    }
}