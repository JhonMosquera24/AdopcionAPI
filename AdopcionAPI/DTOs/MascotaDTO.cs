using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopcionAPI.DTOs
{
    public class MascotaDTO
    {
        public int Id { get; set; }
        public string Nombre_Animal { get; set; }

        public int EspecieId { get; set; }

        public int CentroId { get; set; }
        

        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Raza { get; set; }
        public DateTime FechaNac { get; set; }

        public DateTime FechaIng { get; set; }

        public int Peso { get; set; }
    }
}
