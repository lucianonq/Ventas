using System;
using System.Linq;
using System.Web.Mvc;
using Ventas.Models;
using Ventas.ViewModels;

namespace Ventas.Controllers
{
    public class VentasController : Controller
    {
        VentasDbContext _context = new VentasDbContext();

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Lista()
        {
            var lista = _context.Ventas.ToList();
            var productos = _context.Ventas_renglon.ToList();

            var viewModel = new VentasListadoViewModel
            {
                Ventas = lista,
                Productos = productos
            };

            return View(viewModel);
        }

        public ActionResult Nuevo()
        {
            var empleados = _context.Empleados.AsEnumerable().Select(s => new { EmpleadoId = s.Id, EmpleadoDescrip = $"{s.Apellido}, {s.Nombre}" }).ToList();
            var clientes = _context.Clientes.AsEnumerable().Select(s => new { ClienteId = s.Id, ClienteDescrip = $"{s.Apellido}, {s.Nombre}" }).ToList();

            ViewBag.EmpleadoID = new SelectList(empleados, "EmpleadoId", "EmpleadoDescrip");
            ViewBag.ClienteId = new SelectList(clientes, "ClienteId", "ClienteDescrip");

            return View("Venta");
        }

        public ActionResult AgregarProducto(int id)
        {
            var productosVacio = new DetalleVentaViewModel()
            {
                VentaId = id,
                ProductosListado = _context.Productos.ToList()
            };

            return View("Productos", productosVacio);
        }

        public ActionResult GuardarVenta(VentaViewModel ventaVM)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Nuevo");

            var venta = new Models.Ventas()
            {
                ClienteId = ventaVM.ClienteId,
                EmpleadoId = ventaVM.EmpleadoId,
                Fecha = DateTime.Now,
            };

            _context.Ventas.Add(venta);

            _context.SaveChanges();

            return RedirectToAction("AgregarProducto", new { id = _context.Ventas.Max(x => x.Id) });
        }

        public ActionResult GuardarProducto(DetalleVentaViewModel ventaVM)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Lista");

            var venta = new Ventas_renglon()
            {
                ProductoId = ventaVM.ProductoId,
                Cantidad = ventaVM.Cantidad,
                VentaId = ventaVM.VentaId
            };

            _context.Ventas_renglon.Add(venta);

            _context.SaveChanges();

            return RedirectToAction("AgregarProducto", new { id = ventaVM.VentaId });
        }

        public ActionResult Imprimir(int id)
        {
            var venta = _context.Ventas.FirstOrDefault(x => x.Id == id);
            var productos = _context.Ventas_renglon.Where(x => x.VentaId == id);

            var viewModel = new VentasListadoViewModel()
            {
                Venta = venta,
                Productos = productos
            };

            return View(viewModel);
        }

        public ActionResult Borrar(int id)
        {
            var venta = _context.Ventas.FirstOrDefault(v => v.Id == id);
            var productos = _context.Ventas_renglon.Where(p => p.VentaId == id);

            foreach (var item in productos)
            {
                _context.Ventas_renglon.Remove(item);
            }

            _context.Ventas.Remove(venta);
            _context.SaveChanges();

            return RedirectToAction("Lista");
        }
    }
}