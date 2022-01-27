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
    [Route("api/adopciones")]
    public class AdopcionController : ControllerBase
    {
        private readonly AplicationDbcontext context;
        private readonly IMapper mapper;

        public AdopcionController(AplicationDbcontext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<AdopcionDTO>>> Get()
        {
            var adopciones = await context.Adopciones.ToListAsync();

            var adopcionesDTO = mapper.Map<List<AdopcionDTO>>(adopciones);

            return adopcionesDTO;

        }

        [HttpGet("{id:int}")] 
         public async  Task<ActionResult<AdopcionDTO>> Get(int id)
         {
            var adopcion = await context.Adopciones.FirstOrDefaultAsync(adopcionDB => adopcionDB.AdopcionId == id);

            if (adopcion == null)
            {
                return NotFound();
            }

            var adopcionDTO = mapper.Map<AdopcionDTO>(adopcion);
            return adopcionDTO;

         }

         
         [HttpGet("{id:int}/mascota/adoptante")]
         public async Task<ActionResult<AdopcionConsultaConMascotaAdoptante>> Get(int id , [FromQuery] bool conMascota = true, bool conAdoptante = true)
         {
            var adopcion = await context.Adopciones.FirstOrDefaultAsync(adopcionDB => adopcionDB.AdopcionId == id);

                if (adopcion == null)
                {
                    return NotFound();
                }

                var consultadetalladaDTO = mapper.Map<AdopcionConsultaConMascotaAdoptante>(adopcion);
                var mascota = new Mascota();
                
                if (conMascota)
                {
                mascota = await context.Animales.FirstOrDefaultAsync(mascotaDB => mascotaDB.Id == adopcion.MascotaId);
                consultadetalladaDTO.Mascota = mascota;
                }

                var adoptante = new Adoptante();
                if (conAdoptante)
                {
                adoptante = await context.Adoptantes.FirstOrDefaultAsync(adptanteDB => adptanteDB.AdoptanteId == adopcion.AdoptanteId);
                consultadetalladaDTO.Adoptante = adoptante;
                }

            return consultadetalladaDTO;

         }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AdopcionCreacionDTO adopcionCreacionDTO)
        {
            var existeMascota = await context.Animales.AnyAsync(mascotaDB => mascotaDB.Id == adopcionCreacionDTO.MascotaId);
            if (!existeMascota)
            {
                return BadRequest($"No se encuentra la Mascota con el ID{adopcionCreacionDTO.MascotaId}");
            }


            var existeAdoptante = await context.Adoptantes.AnyAsync(adoptanteDB => adoptanteDB.AdoptanteId == adopcionCreacionDTO.AdoptanteId);

            if (!existeAdoptante)
            {
                return BadRequest($"No se encuentra el adoptante con el ID{adopcionCreacionDTO.AdoptanteId}");
            }


            var adopcion = mapper.Map<Adopcion>(adopcionCreacionDTO);
            context.Add(adopcion);
            await context.SaveChangesAsync();

            return Ok();
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult>Put(int id, [FromBody] AdopcionCreacionDTO adopcionCreacionDTO)
        {

            var existeAdopcion = await context.Adopciones.AnyAsync(adopcionDB => adopcionDB.AdopcionId == id);
            if (!existeAdopcion)
            {
                return BadRequest($"No se encontro una adopcion con el ID {id}");
            }

            var existeMascota = await context.Animales.AnyAsync(mascotaDB => mascotaDB.Id ==  adopcionCreacionDTO.MascotaId);
            if (!existeMascota)
            {
                return   BadRequest($"No se encontro la mascota con el ID {adopcionCreacionDTO.MascotaId}");
            }

            var existeAdoptante = await context.Adoptantes.AnyAsync(adoptanteDB => adoptanteDB.AdoptanteId == adopcionCreacionDTO.AdoptanteId);
            if (!existeAdoptante)
            {
                return BadRequest($"No se encontro el adoptante con el ID {adopcionCreacionDTO.AdoptanteId} ");
            }

            var adopcion = mapper.Map<Adopcion>(adopcionCreacionDTO);
            adopcion.AdopcionId = id;
            context.Entry(adopcion).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();


        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) {

            var existeAdopcion = await context.Adopciones.AnyAsync(adopcionDB => adopcionDB.AdopcionId == id);

            if (!existeAdopcion)
            {
                return BadRequest($"No Existe una adopcion con el ID{id}");
            }


            
            context.Remove(new Adopcion() { AdopcionId = id });
            await context.SaveChangesAsync();
            return NoContent();


        }





    }
}
