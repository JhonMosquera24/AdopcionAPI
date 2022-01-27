using AdopcionAPI.DTOs;
using AdopcionAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopcionAPI.Controllers
{

    [ApiController]
    [Route("api/adoptantes")]
    public class AdoptanteController : ControllerBase
    {
        private readonly AplicationDbcontext context;
        private readonly IMapper mapper;

        public AdoptanteController(AplicationDbcontext context , IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AdoptanteSinAdopcionesDTO>>> Get()
        {
            var adoptante = await context.Adoptantes.ToListAsync();

            var adoptanteDTO = mapper.Map <List<AdoptanteSinAdopcionesDTO>>(adoptante); 
            return adoptanteDTO;

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AdoptanteSinAdopcionesDTO>>Get(int id)
        {
            var adoptantes = await context.Adoptantes.FirstOrDefaultAsync(adoptanteDB => adoptanteDB.AdoptanteId == id);
           
            if (adoptantes == null)
            {
                return NotFound();
            }

            var adoptanteDTO = mapper.Map<AdoptanteSinAdopcionesDTO>(adoptantes);
            return adoptanteDTO;
            
        }

        [HttpGet("{id:int}/adopciones")]
        public async Task<ActionResult<AdoptanteDTO>>Get(int id, [FromQuery] bool conAdopciones = true)
        {
            var adoptantes = await context.Adoptantes.FirstOrDefaultAsync(adoptantesDB => adoptantesDB.AdoptanteId == id);
            if (adoptantes == null)
            {
                return NotFound();
            }

            if (!conAdopciones)
            {
                var adoptantesSinAdopcion = mapper.Map<AdoptanteDTO>(adoptantes);
                return adoptantesSinAdopcion;
            }
            else
            {
                var adopciones = await context.Adopciones.Where(adopcionesDB => adopcionesDB.AdoptanteId == id).ToListAsync();
                adoptantes.Adopciones = adopciones;
                var adoptantesConAdopcion = mapper.Map<AdoptanteDTO>(adoptantes);
                return adoptantesConAdopcion;
            }

        }

        [HttpPost] 
        public async Task<ActionResult> Post([FromBody] AdoptanteCreacionDTO adoptanteCreacionDTO)
        {
            var existeDocumento = await context.Adoptantes.AnyAsync(adoptanteDB => adoptanteDB.DocumentoIdentidad == adoptanteCreacionDTO.DocumentoIdentidad);
            if (existeDocumento)
            {
                return BadRequest($"Ya existe un adoptante con el numero de identidad {adoptanteCreacionDTO.DocumentoIdentidad}");
            }

            var adoptante = mapper.Map<Adoptante>(adoptanteCreacionDTO);
            context.Add(adoptante);
            await context.SaveChangesAsync();
            return Ok();

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] AdoptanteCreacionDTO adoptanteCreacionDTO)
        {
            var existeAdoptante = await context.Adoptantes.AnyAsync(adoptanteDB => adoptanteDB.AdoptanteId == id);
            if (!existeAdoptante)
            {
                return BadRequest($"No existe el adoptante con el ID {id}");
            }

            var adoptante = mapper.Map<Adoptante>(adoptanteCreacionDTO);
            adoptante.AdoptanteId = id;
            context.Entry(adoptante).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {

            var existeAdoptante = await context.Adoptantes.AnyAsync(adoptanteDB => adoptanteDB.AdoptanteId == id);
            if (!existeAdoptante)
            {
                return BadRequest($"No existe el adoptante con el ID {id}");
            }


            context.Remove(new Adoptante() { AdoptanteId = id });
            await context.SaveChangesAsync();
            return NoContent();


        }


    }
}
