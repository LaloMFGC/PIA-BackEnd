using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIAWebApi.DTO;
using PIAWebApi.Entidades;

namespace PIAWebApi.Controllers
{

    [ApiController]
    [Route("api/participantes")]
    public class ParticipantesController: ControllerBase 
    {
        private readonly ApplicationDbContext dbcontext;
        private readonly IMapper mapper;

        public ParticipantesController(ApplicationDbContext dbcontext, IMapper mapper)
        {
            this.dbcontext = dbcontext;
            this.mapper = mapper;
        }


        [HttpGet("{id:int}", Name = "ObtenerParticipante")]
        public async Task<ActionResult<ParticipantesDTOConRifas>> Get(int id)
        {
            var participante = await dbcontext.Participantes
                .Include(participanteDB => participanteDB.RifasParticipantes)
                .ThenInclude(rifasparticipanteDB => rifasparticipanteDB.Rifa)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (participante == null)
            {
                return NotFound();
            }

            participante.RifasParticipantes = participante.RifasParticipantes.OrderBy(x => x.Orden).ToList();

            return mapper.Map<ParticipantesDTOConRifas>(participante);
        }




        [HttpPost]
        public async Task<ActionResult> Post(ParticipantesCreacionDTO participanteCreacionDTO)
        {
            if (participanteCreacionDTO.RifasIds == null)
            {
                return BadRequest("No se puede crear un libro sin autores");
            }

            var rifasIds = await dbcontext.Rifas
                .Where(rifaBD => participanteCreacionDTO.RifasIds.Contains(rifaBD.Id)).Select(x => x.Id).ToListAsync();

            if (participanteCreacionDTO.RifasIds.Count != rifasIds.Count)
            {
                return BadRequest("No existe uno de los autores enviados");
            }

            var participante = mapper.Map<Participante>(participanteCreacionDTO);
            AsignarOrdenRifas(participante);

            dbcontext.Add(participante);
            await dbcontext.SaveChangesAsync();

            var participanteDTO = mapper.Map<ParticipantesDTO>(participante);

            return CreatedAtRoute("ObtenerParticipante", new { id = participante.Id }, participanteDTO);
        }




        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, ParticipantesCreacionDTO participanteCreacionDTO)
        {
            var participanteDB = await dbcontext.Participantes
                .Include(x => x.RifasParticipantes)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (participanteDB == null)
            {
                return NotFound();
            }

            participanteDB = mapper.Map(participanteCreacionDTO, participanteDB);

            AsignarOrdenRifas(participanteDB);

            await dbcontext.SaveChangesAsync();
            return NoContent();
        }



        private void AsignarOrdenRifas(Participante participante)
        {
            if (participante.RifasParticipantes != null)
            {
                for (int i = 0; i < participante.RifasParticipantes.Count; i++)
                {
                    participante.RifasParticipantes[i].Orden = i;
                }
            }

        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<ParticipantePatchDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var participanteDB = await dbcontext.Participantes.FirstOrDefaultAsync(x => x.Id == id);

            if (participanteDB == null)
            {
                return NotFound();
            }

            var participanteDTO = mapper.Map<ParticipantePatchDTO>(participanteDB);

            patchDocument.ApplyTo(participanteDTO, ModelState);

            var esValido = TryValidateModel(participanteDTO);

            if (!esValido)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(participanteDTO, participanteDB);

            await dbcontext.SaveChangesAsync();
            return NoContent();
        }




        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await dbcontext.Participantes.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            dbcontext.Remove(new Participante() { Id = id });
            await dbcontext.SaveChangesAsync();
            return NoContent();
        }
    }
}
