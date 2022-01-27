using AdopcionAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdopcionAPI.DTOs
{
    public class CentroConsultaConMascotasDTO
    {

        public int Id { get; set; }
        [Required]
        [StringLength(158)]
        public string NombreCentro { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public int CiudadId { get; set; }

        public virtual Ciudad Ciudad { get; set; }

        public string NombreEmpleado { get; set; }

        public List<Mascota> Mascotas { get; set; }
    }
}
