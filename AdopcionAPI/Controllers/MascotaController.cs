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
    [Route("api/mascotas")]
    public class MascotaController: ControllerBase
    {
        private readonly AplicationDbcontext contex;
        private readonly IMapper mapper;

        public MascotaController(AplicationDbcontext contex, 
            IMapper mapper)
        {
            this.contex = contex;
            this.mapper = mapper;
        }



         [HttpGet]
         public async Task<ActionResult<List<MascotaDTO>>> Get()
        {
            var mascotas = await contex.Animales.ToListAsync();

            return mapper.Map<List<MascotaDTO>>(mascotas);
        }

        [HttpGet("{id:int}", Name ="obtenerMascota")]
        public async Task<ActionResult<MascotaDTO>> Get(int id)
        {
            var mascota = await contex.Animales.FirstOrDefaultAsync(mascotaDB => mascotaDB.Id == id);

            if (mascota == null)
            {
                return NotFound();
            }

            return mapper.Map<MascotaDTO>(mascota);
        }



        [HttpGet("{id:int}/centro", Name ="obtenerMascotaConCentro")]
        public async Task<ActionResult<MascotaConsultaConCentroDTO>>Get(int id, [FromQuery] bool conCentro = true)
        {
            var mascota = await contex.Animales.FirstOrDefaultAsync(mascotaDB => mascotaDB.Id == id);

            if (mascota == null)
            {
                return NotFound();
            }

            var mascotaConsultaDTO = mapper.Map<MascotaConsultaConCentroDTO>(mascota);
            var centro = new Centro();
            if (conCentro)
            {
                centro = await contex.Centros.FirstOrDefaultAsync(centroDB => centroDB.Id == mascota.CentroId);
                mascotaConsultaDTO.Centro = centro;
            }


            if (true)
            {

            }
            return mascotaConsultaDTO;

        }





        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MascotaCreacionDTO mascotaCreacionDTO)
        {
            var existe = await contex.Centros.AnyAsync(centroDB => centroDB.Id == mascotaCreacionDTO.CentroId);

            if (!existe)
            {
                return BadRequest($"No Existe un centro con el ID{mascotaCreacionDTO.CentroId}");
            }

            var mascota = mapper.Map<Mascota>(mascotaCreacionDTO);
            contex.Add(mascota);
            await contex.SaveChangesAsync();

            return Ok();
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] MascotaCreacionDTO mascotaCreacionDTO)
        {
            var existe = await contex.Animales.AnyAsync(mascotaDB => mascotaDB.Id == id);

            if (!existe)
            {
                return BadRequest($"No existe la mascota con el ID {id}");
            }

            var existeCentro = await contex.Centros.AnyAsync(centroDB => centroDB.Id == mascotaCreacionDTO.CentroId);

            if (!existeCentro)
            {
                return BadRequest($"No existe el centro con el ID {mascotaCreacionDTO.CentroId}");
            }

            var mascota = mapper.Map<Mascota>(mascotaCreacionDTO);
            mascota.Id = id;
            contex.Entry(mascota).State = EntityState.Modified;
            await contex.SaveChangesAsync();
            return NoContent();


        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await contex.Animales.AnyAsync(mascotaDB => mascotaDB.Id ==  id);
            if (!existe)
            {
                return BadRequest($"No se encontro una mascota con el ID {id}");
            }

            contex.Remove(new Mascota() { Id = id });
            await contex.SaveChangesAsync();
            return NoContent();



        }



    }
}
