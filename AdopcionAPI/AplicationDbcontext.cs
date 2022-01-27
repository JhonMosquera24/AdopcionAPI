using AdopcionAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopcionAPI
{
    public class AplicationDbcontext:DbContext
    {
        public AplicationDbcontext(DbContextOptions options): base(options)
        {

        }

        // Configurar todos los modelos del proyecto

        public  DbSet<Mascota> Animales{ get; set; }
        public DbSet<Centro> Centros { get; set; }
        public DbSet<Especie> Especies { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }

        public DbSet<Adopcion> Adopciones { get; set; }

        public DbSet<Adoptante> Adoptantes { get; set; }

    }
}
