using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdopcionAPI.DTOs
{
    public class CentroDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(158)]
        public string NombreCentro { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public int CiudadId { get; set; }

        

        public string NombreEmpleado { get; set; }
    }
}
