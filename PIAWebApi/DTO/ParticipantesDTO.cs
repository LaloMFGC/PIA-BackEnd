namespace PIAWebApi.DTO
{
    public class ParticipantesDTO
    {

        public int Id { get; set; }
        public string Username { get; set; }

        public bool Ganador { get; set; }

        public int LoteriaId { get; set; }

        public string Correo { get; set; }
        public string Telefono { get; set; }

    }
}
