using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ventas.Models;

namespace Ventas.ViewModels
{
    public class VentasListadoViewModel
    {
        public IEnumerable<Models.Ventas> Ventas { get; set; }
        public IEnumerable<Ventas_renglon> Productos { get; set; }
        public Models.Ventas Venta { get; set; }

        public VentasListadoViewModel()
        {

        }
    }
}