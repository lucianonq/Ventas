using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Ventas.Models;

namespace Ventas.ViewModels
{
    public class EmpleadoViewModel
    {
        public int? Id { get; set; }
        [Required] public string Nombre { get; set; }
        [Required] public string Apellido { get; set; }
        [Required] public int DNI { get; set; }
        [Required][DisplayName("Fecha de Nacimiento")] public System.DateTime FecNac { get; set; }
        [Required] public string Legajo { get; set; }

        public EmpleadoViewModel()
        {

        }

        public EmpleadoViewModel(Empleados empleado)
        {
            Id = empleado.Id;
            Nombre = empleado.Nombre;
            Apellido = empleado.Apellido;
            DNI = empleado.DNI;
            FecNac = empleado.FecNac;
            Legajo = empleado.Legajo;
        }
    }
}