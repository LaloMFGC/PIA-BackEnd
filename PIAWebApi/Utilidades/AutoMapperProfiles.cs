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
        }

    }
}
