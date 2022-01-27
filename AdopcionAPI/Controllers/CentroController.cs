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
    [Route("api/centros")]
    public class CentroController : ControllerBase
    {
        private readonly AplicationDbcontext context;
        private readonly IMapper mapper;

        public CentroController(AplicationDbcontext context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<CentroDTO>>> Get()
        {
            var centros = await context.Centros.ToListAsync();

            var centrosDTO = mapper.Map<List<CentroDTO>>(centros);

            return centrosDTO;
        }

        [HttpGet("{id:int}", Name = "obtenerCentro")]
        public async Task<ActionResult<CentroDTO>> Get(int id)
        {
            var centro = await context.Centros.FirstOrDefaultAsync(centroDB => centroDB.Id == id);
            if (centro == null)
            {
                return NotFound();
            }

            var centroDTO = mapper.Map<CentroDTO>(centro);

            return centroDTO;
            
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<CentroConsultaDetalladaDTO>>> Get(string nombre)
        {
            var centros = await context.Centros.Where(centroDB => centroDB.NombreCentro.Contains(nombre)).ToListAsync();

            var centrosDTO = mapper.Map<List<CentroConsultaDetalladaDTO>>(centros);
            return centrosDTO;

        }

        [HttpGet("{id:int}/mascotas")]
        public async Task<ActionResult<CentroConsultaConMascotasDTO>> Get(int id, [FromQuery] bool conMascotas = true)
        {
            Centro centro = new Centro();
            if (!conMascotas)
            {
                 centro = await context.Centros.FirstOrDefaultAsync(centroDB => centroDB.Id == id);
            }
            else
            {
                centro = await context.Centros
                   .Include(centroDB => centroDB.Mascotas)
                   .FirstOrDefaultAsync(centroDB => centroDB.Id == id);
            }

            if (centro == null)
            {
                return NotFound();

            }

            var centroConsultaDTO = mapper.Map<CentroConsultaConMascotasDTO>(centro);
            return centroConsultaDTO;




        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CentroCreacionDTO centroCreacionDTO)
        {
            var existeCentroMismoNombre = await context.Centros.AnyAsync(x => x.NombreCentro == centroCreacionDTO.NombreCentro);


                if (!existeCentroMismoNombre)
                {
                return BadRequest($"Existe un centro con el nombre {centroCreacionDTO.NombreCentro}");
                }

            var centro = mapper.Map<Centro>(centroCreacionDTO);

            context.Add(centro);
            await context.SaveChangesAsync();

            var centroDTO = mapper.Map<CentroDTO>(centro);

            return new CreatedAtRouteResult("obtenerCentro", new { id = centroDTO.Id }, centroDTO);


        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CentroCreacionDTO centroCreacionDTO)
        {
            var existeCentro = await context.Centros.AnyAsync(CentroDB => CentroDB.Id == id);


            if (!existeCentro)
            {
                return NotFound();
            }

            var centro = mapper.Map<Centro>(centroCreacionDTO);
            centro.Id = id;
            context.Entry(centro).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existeCentro = await context.Centros.AnyAsync(CentroDB => CentroDB.Id == id);


            if (!existeCentro)
            {
                return NotFound();
            }



            context.Remove(new Centro() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();

        }
    }
}
