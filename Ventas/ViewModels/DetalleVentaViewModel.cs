using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ventas.Models;

namespace Ventas.ViewModels
{
    public class DetalleVentaViewModel
    {
        public IEnumerable<Productos> ProductosListado { get; set; }

        public int? Id { get; set; }
        public int VentaId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        public DetalleVentaViewModel()
        {

        }
    }
}