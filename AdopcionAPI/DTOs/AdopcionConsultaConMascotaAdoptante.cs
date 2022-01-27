using AdopcionAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdopcionAPI.DTOs
{
    public class AdopcionConsultaConMascotaAdoptante
    {

        public int AdopcionId { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        

        public Mascota Mascota { get; set; }

        [Required]
        
        public Adoptante Adoptante { get; set; }
    }
}
