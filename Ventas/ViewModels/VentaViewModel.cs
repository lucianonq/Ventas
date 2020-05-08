using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Ventas.Models;

namespace Ventas.ViewModels
{
    public class VentaViewModel
    {
        public IEnumerable<Empleados> EmpleadosListado { get; set; }
        public IEnumerable<Clientes> ClientesListado { get; set; }

        public int? Id { get; set; }
        [Required]
        [DisplayName("Cliente")]
        public int ClienteId { get; set; }
        [Required]
        [DisplayName("Empleado")]
        public int EmpleadoId { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }

        public VentaViewModel()
        {

        }
    }
}