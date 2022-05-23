namespace PIAWebApi.Entidades
{
    public class RifasParticipantes
    {

        public int ParticipanteId { get; set; }
        public int RifaId { get; set; }
        public int Orden { get; set; }
        public Participante Participante { get; set; }
        public Rifa Rifa { get; set; }

    }
}
