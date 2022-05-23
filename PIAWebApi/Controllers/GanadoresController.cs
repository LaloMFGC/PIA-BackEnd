using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIAWebApi.DTO;
using PIAWebApi.Entidades;

namespace PIAWebApi.Controllers
{


    [ApiController]
    [Route("api/participantes/{participantesId:int}/ganadores")]
    public class GanadoresController: ControllerBase
    {
        private readonly ApplicationDbContext dbcontext;
        private readonly IMapper mapper;

        public GanadoresController(ApplicationDbContext dbcontext, IMapper mapper)
        {
            this.dbcontext = dbcontext;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<GanadoresDTO>>> Get(int participanteId)
        {
            var existeParticipante = await dbcontext.Participantes.AnyAsync(participanteDB => participanteDB.Id == participanteId);

            if (!existeParticipante)
            {
                return NotFound();
            }

            var ganadores = await dbcontext.Ganadores
                .Where(ganadoresDB => ganadoresDB.ParticipanteId == participanteId).ToListAsync();

            return mapper.Map<List<GanadoresDTO>>(ganadores);
        }


        [HttpGet("{id:int}", Name = "ObtenerGanadores")]
        public async Task<ActionResult<GanadoresDTO>> GetPorId(int id)
        {
            var ganadores = await dbcontext.Ganadores.FirstOrDefaultAsync(ganadoresDB => ganadoresDB.Id == id);

            if (ganadores == null)
            {
                return NotFound();
            }

            return mapper.Map<GanadoresDTO>(ganadores);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int participanteId, GanadoresCreacionDTO ganadoresCreacionDTO)
        {
            var existeParticipante = await dbcontext.Participantes.AnyAsync(participanteDB => participanteDB.Id == participanteId);

            if (!existeParticipante)
            {
                return NotFound();
            }

            var ganadores = mapper.Map<Ganadores>(ganadoresCreacionDTO);
            ganadores.ParticipanteId = participanteId;
            dbcontext.Add(ganadores);
            await dbcontext.SaveChangesAsync();

            var ganadoresDTO = mapper.Map<GanadoresDTO>(ganadores);

            return CreatedAtRoute("ObtenerGanadores", new { id = ganadores.Id, participanteId = participanteId }, ganadoresDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int participanteId, int id, GanadoresCreacionDTO ganadoresCreacionDTO)
        {
            var existeParticipante = await dbcontext.Participantes.AnyAsync(participanteDB => participanteDB.Id == participanteId);

            if (!existeParticipante)
            {
                return NotFound();
            }

            var existeComentario = await dbcontext.Ganadores.AnyAsync(ganadoresDB => ganadoresDB.Id == id);

            if (!existeComentario)
            {
                return NotFound();
            }

            var ganadores = mapper.Map<Ganadores>(ganadoresCreacionDTO);
            ganadores.Id = id;
            ganadores.ParticipanteId = participanteId;
            dbcontext.Update(ganadores);
            await dbcontext.SaveChangesAsync();
            return NoContent();
        }


    }
}
