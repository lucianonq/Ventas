using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using Ventas.Models;
using Ventas.ViewModels;

namespace Ventas.Controllers
{
    public class ProductosController : Controller
    {
        VentasDbContext _context = new VentasDbContext();

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Lista()
        {
            var lista = _context.Productos.ToList();

            return View(lista);
        }

        public ActionResult Nuevo()
        {
            var productoVacio = new ProductoViewModel();

            return View("Producto", productoVacio);
        }

        public ActionResult Actualizar(int id)
        {
            var producto = _context.Productos.FirstOrDefault(x => x.Id == id);

            if (producto == null)
                return null;

            var productoVM = new ProductoViewModel
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Marca = producto.Marca,
                FecVenc = producto.FecVenc,
                Precio = producto.Precio,
                Proveedor = producto.Proveedor
            };

            return View("Producto", productoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guardar(ProductoViewModel productoVM)
        {
            if (!ModelState.IsValid)
            {
                if (productoVM.Id == null)
                    return RedirectToAction("Nuevo");
                else
                    return RedirectToAction("Actualizar", new { id = productoVM.Id });
            }


            if (productoVM.Id == null)
            {
                var producto = new Productos()
                {
                    Nombre = productoVM.Nombre,
                    Marca = productoVM.Marca,
                    FecVenc = productoVM.FecVenc,
                    Precio = productoVM.Precio,
                    Proveedor = productoVM.Proveedor
                };

                _context.Productos.Add(producto);
            }
            else
            {
                var productoEnDb = _context.Productos.FirstOrDefault(x => x.Id == productoVM.Id);
                productoEnDb.Nombre = productoVM.Nombre;
                productoEnDb.Marca = productoVM.Marca;
                productoEnDb.FecVenc = productoVM.FecVenc;
                productoEnDb.Precio = productoVM.Precio;
                productoEnDb.Proveedor = productoVM.Proveedor;
            }

            _context.SaveChanges();

            return RedirectToAction("Lista");
        }

        public ActionResult Borrar(int id)
        {
            var producto = _context.Productos.FirstOrDefault(x => x.Id == id);

            if (producto == null)
                return RedirectToAction("Lista");

            try
            {
                _context.Productos.Remove(producto);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                ViewBag.Error = "Producto ya asociado a una venta. Elimine la factura para proceder.";
            }

            var lista = _context.Productos.ToList();
            return View("Lista", lista);
        }
    }
}