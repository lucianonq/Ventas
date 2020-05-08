using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using Ventas.Models;
using Ventas.ViewModels;

namespace Ventas.Controllers
{
    public class ClientesController : Controller
    {
        VentasDbContext _context = new VentasDbContext();

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Lista()
        {
            var lista = _context.Clientes.ToList();

            return View(lista);
        }

        public ActionResult Nuevo()
        {
            var clienteVacio = new ClienteViewModel();

            return View("Cliente", clienteVacio);
        }

        public ActionResult Actualizar(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(x => x.Id == id);

            if (cliente == null)
                return RedirectToAction("Nuevo");

            var clienteVM = new ClienteViewModel
            {
                Id = cliente.Id,
                Apellido = cliente.Apellido,
                Nombre = cliente.Nombre,
                DNI = cliente.DNI,
                FecNac = cliente.FecNac,
                Tarjeta = cliente.Tarjeta
            };

            return View("Cliente", clienteVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guardar(ClienteViewModel clienteVM)
        {
            if (!ModelState.IsValid)
            {
                if (clienteVM.Id == null)
                    return RedirectToAction("Nuevo");
                else
                    return RedirectToAction("Actualizar", new { id = clienteVM.Id });
            }

            if (clienteVM.Id == null)
            {
                var cliente = new Clientes()
                {
                    Apellido = clienteVM.Apellido,
                    Nombre = clienteVM.Nombre,
                    DNI = clienteVM.DNI,
                    FecNac = clienteVM.FecNac,
                    Tarjeta = clienteVM.Tarjeta
                };

                _context.Clientes.Add(cliente);
            }
            else
            {
                var clienteEnDb = _context.Empleados.FirstOrDefault(x => x.Id == clienteVM.Id);
                clienteEnDb.Nombre = clienteVM.Nombre;
                clienteEnDb.Apellido = clienteVM.Apellido;
                clienteEnDb.DNI = clienteVM.DNI;
                clienteEnDb.FecNac = clienteVM.FecNac;
            }

            _context.SaveChanges();

            return RedirectToAction("Lista");
        }

        public ActionResult Borrar(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(x => x.Id == id);

            if (cliente == null)
                return RedirectToAction("Lista");

            try
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                ViewBag.Error = "Cliente ya asociado a una venta. Elimine la factura para proceder.";
            }

            var lista = _context.Clientes.ToList();
            return View("Lista", lista);
        }
    }
}