using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIAWebApi.DTO;
using PIAWebApi.Entidades;

namespace PIAWebApi.Controllers
{

    [ApiController]
    [Route("api/rifas")]
    public class RifasController: ControllerBase
    {
        private readonly ApplicationDbContext dbcontext;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public RifasController(ApplicationDbContext dbcontext, IMapper mapper)
        {
            this.dbcontext = dbcontext;
            this.mapper = mapper;
        }

        

        //[HttpGet("configuraciones")]
        //public ActionResult<string> ObtenerConfiguracion()
        //{
        //    return configuration["apellido"];
        //}


        [HttpGet] // api/autores
        public async Task<List<RifasDTO>> Get()
        {
            var rifas = await dbcontext.Rifas.ToListAsync();
            return mapper.Map<List<RifasDTO>>(rifas);
        }


        [HttpGet("{id:int}", Name = "obtenerRifa")]
        public async Task<ActionResult<RifasDTOConParticipantes>> Get(int id)
        {
            var rifa = await dbcontext.Rifas
                .Include(rifaDB => rifaDB.RifasParticipantes)
                .ThenInclude(rifasparticipantesDB => rifasparticipantesDB.Participante)
                .FirstOrDefaultAsync(rifasBD => rifasBD.Id == id);

            if (rifa == null)
            {
                return NotFound();
            }

            return mapper.Map<RifasDTOConParticipantes>(rifa);
        }


        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<RifasDTO>>> Get([FromRoute] string nombre)
        {
            var rifas = await dbcontext.Rifas.Where(rifaBD => rifaBD.NameRifa.Contains(nombre)).ToListAsync();

            return mapper.Map<List<RifasDTO>>(rifas);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RifasCreacionDTO rifaCreacionDTO)
        {
            var existerifa = await dbcontext.Rifas.AnyAsync(x => x.NameRifa == rifaCreacionDTO.NameRifa);

            if (existerifa)
            {
                return BadRequest($"Ya existe un autor con el nombre {rifaCreacionDTO.NameRifa}");
            }

            var rifa = mapper.Map<Rifa>(rifaCreacionDTO);

            dbcontext.Add(rifa);
            await dbcontext.SaveChangesAsync();

            var rifaDTO = mapper.Map<RifasDTO>(rifa);

            return CreatedAtRoute("obtenerRifa", new { id = rifa.Id }, rifaDTO);
        }



        [HttpPut("{id:int}")] // api/autores/1 
        public async Task<ActionResult> Put(RifasCreacionDTO rifaCreacionDTO, int id)
        {
            var existe = await dbcontext.Rifas.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            var rifa = mapper.Map<Rifa>(rifaCreacionDTO);
            rifa.Id = id;

            dbcontext.Update(rifa);
            await dbcontext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")] // api/autores/2
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await dbcontext.Rifas.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            dbcontext.Remove(new Rifa() { Id = id });
            await dbcontext.SaveChangesAsync();
            return NoContent();
        }




    }
}
