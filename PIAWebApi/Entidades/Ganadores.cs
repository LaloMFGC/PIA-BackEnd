namespace PIAWebApi.Entidades
{
    public class Ganadores
    {
        public int Id                                       { get; set; }
        public string Premios                               { get; set; }
        public int ParticipanteId                           { get; set; }
        public Participante participante                    { get; set; }

    }
}
