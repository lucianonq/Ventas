using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Ventas.Models;

namespace Ventas.ViewModels
{
    public class ProductoViewModel
    {
        public int? Id { get; set; }
        [Required] public string Nombre { get; set; }
        [Required] public string Marca { get; set; }
        [Required][DisplayName("Fecha de Vencimiento")] public Nullable<System.DateTime> FecVenc { get; set; }
        [Required] public Nullable<decimal> Precio { get; set; }
        [Required] public string Proveedor { get; set; }

        public ProductoViewModel()
        {

        }

        public ProductoViewModel(Productos producto)
        {
            Id = producto.Id;
            Nombre = producto.Nombre;
            Marca = producto.Marca;
            FecVenc = producto.FecVenc;
            Precio = producto.Precio;
            Proveedor = producto.Proveedor;
        }
    }
}