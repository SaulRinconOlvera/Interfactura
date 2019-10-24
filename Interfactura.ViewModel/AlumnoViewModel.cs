using Interfactura.ViewModel.Base;
using Interfactura.ViewModel.enumerations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Interfactura.ViewModel
{
    public class AlumnoViewModel : IBaseViewModel
    {
        public AlumnoViewModel() 
        {
            Id = Guid.NewGuid();
            Activo = true;
            FechaNacimiento = FechaIngreso = DateTime.Now;
        }
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^(([A-ZÑ&]{4})([0-9]{2})([0][13578]|[1][02])(([0][1-9]|[12][\\d])|[3][01])([A-Z0-9]{3}))|" +
                       "(([A-ZÑ&]{4})([0-9]{2})([0][13456789]|[1][012])(([0][1-9]|[12][\\d])|[3][0])([A-Z0-9]{3}))|" +
                       "(([A-ZÑ&]{4})([02468][048]|[13579][26])[0][2]([0][1-9]|[12][\\d])([A-Z0-9]{3}))|" +
                       "(([A-ZÑ&]{4})([0-9]{2})[0][2]([0][1-9]|[1][0-9]|[2][0-8])([A-Z0-9]{3}))$", ErrorMessage = "Debe ingresar un RFC válido")]
        public string RFC { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public GeneroEnum Genero { get; set; }

        [Required]
        public DateTime FechaIngreso { get; set; }
        public bool Activo { get; set; }
    }
}
