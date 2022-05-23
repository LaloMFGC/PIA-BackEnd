using AutoMapper;
using PIAWebApi.DTO;
using PIAWebApi.Entidades;

namespace PIAWebApi.Utilidades
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<RifasCreacionDTO, Rifa>();
            CreateMap<ParticipantesCreacionDTO, Participante>();
        
            CreateMap<RifasCreacionDTO, Rifa>();
            CreateMap<Rifa, RifasDTO>();
            CreateMap<Rifa, RifasDTOConParticipantes>()
                .ForMember(rifaDTO => rifaDTO.Participante, opciones => opciones.MapFrom(MapRifaDTOParticipantes));

            CreateMap<ParticipantesCreacionDTO, Participante>()
                .ForMember(participante => participante.RifasParticipantes, opciones => opciones.MapFrom(MapRifasParticipantes));
            CreateMap<Participante, ParticipantesDTO>().ReverseMap();
            CreateMap<Participante, ParticipantesDTOConRifas>()
                .ForMember(participanteDTO => participanteDTO.Rifa, opciones => opciones.MapFrom(MapParticipanteDTORIfas));
            CreateMap<ParticipantePatchDTO, Participante>().ReverseMap();

            CreateMap<GanadoresCreacionDTO, Ganadores>();
            CreateMap<Ganadores, GanadoresDTO>();
        }

        private List<ParticipantesDTO> MapRifaDTOParticipantes(Rifa rifa, RifasDTO rifasDTO)
        {
            var resultado = new List<ParticipantesDTO>();

            if (rifa.RifasParticipantes == null) { return resultado; }

            foreach (var rifasparticipantes in rifa.RifasParticipantes)
            {
                resultado.Add(new ParticipantesDTO()
                {
                    Id = rifasparticipantes.ParticipanteId,
                    Username = rifasparticipantes.Participante.Username,
                    LoteriaId = rifasparticipantes.Participante.LoteriaId,
                    Ganador = rifasparticipantes.Participante.Ganador,
                    Correo = rifasparticipantes.Participante.Correo,
                    Telefono = rifasparticipantes.Participante.Telefono

                });
            }

            return resultado;
        }

        private List<RifasDTO> MapParticipanteDTORIfas(Participante participante, ParticipantesDTO participanteDTO)
        {
            var resultado = new List<RifasDTO>();

            if (participante.RifasParticipantes == null) { return resultado; }

            foreach (var rifaparticipante in participante.RifasParticipantes)
            {
                resultado.Add(new RifasDTO()
                {
                    Id = rifaparticipante.RifaId,
                    NameRifa = rifaparticipante.Rifa.NameRifa
                });
            }

            return resultado;
        }

        private List<RifasParticipantes> MapRifasParticipantes(ParticipantesCreacionDTO participantesCreacionDTO, Participante participante)
        {
            var resultado = new List<RifasParticipantes>();

            if (participantesCreacionDTO.RifasIds == null) { return resultado; }

            foreach (var RifaId in participantesCreacionDTO.RifasIds)
            {
                resultado.Add(new RifasParticipantes() { RifaId = RifaId });
            }

            return resultado;
        }


    }
}
