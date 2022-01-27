using AdopcionAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdopcionAPI.DTOs
{
    public class AdoptanteSinAdopcionesDTO
    {

        public int AdoptanteId { get; set; }

        [Required]
        public int DocumentoIdentidad { get; set; }

        [Required]
        [StringLength(150)]
        public string NomCompleto { get; set; }

        [Required]
        [StringLength(200)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(20)]
        public string Telefono { get; set; }

        [StringLength(3)]
        public int Edad { get; set; }

        
    }
}
