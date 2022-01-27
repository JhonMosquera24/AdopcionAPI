using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdopcionAPI.DTOs
{
    public class AdopcionCreacionDTO
    {

        public DateTime Fecha { get; set; }

        [Required]
        public int MascotaId { get; set; }

       
        [Required]
        public int AdoptanteId { get; set; }
    }
}
