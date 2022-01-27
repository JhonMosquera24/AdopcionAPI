using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdopcionAPI.DTOs
{
    public class CentroConsultaDetalladaDTO
    {

        public int Id { get; set; }
        [Required]
        [StringLength(158)]
        public string NombreCentro { get; set; }

        
    }
}
