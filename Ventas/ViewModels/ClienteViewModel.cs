using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Ventas.Models;

namespace Ventas.ViewModels
{
    public class ClienteViewModel
    {
        public int? Id { get; set; }
        [Required] public string Nombre { get; set; }
        [Required] public string Apellido { get; set; }
        [Required] public int DNI { get; set; }
        [Required] [DisplayName("Fecha de Nacimiento")] public System.DateTime FecNac { get; set; }
        [DisplayName("Tarjeta de Crédito")] public string Tarjeta { get; set; }

        public ClienteViewModel()
        {

        }

        public ClienteViewModel(Clientes empleado)
        {
            Id = empleado.Id;
            Nombre = empleado.Nombre;
            Apellido = empleado.Apellido;
            DNI = empleado.DNI;
            FecNac = empleado.FecNac;
            Tarjeta = empleado.Tarjeta;
        }
    }
}