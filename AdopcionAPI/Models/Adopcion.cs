using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdopcionAPI.Models
{
    public class Adopcion
    {
        public int AdopcionId { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public int MascotaId { get; set; }

        //public Mascota Mascota { get; set; }

        [Required]
        public int AdoptanteId { get; set; }

        //public Adoptante Adoptante { get; set; }
    }
}
