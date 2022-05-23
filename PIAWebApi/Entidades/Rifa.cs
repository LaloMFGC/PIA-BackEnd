using System.ComponentModel.DataAnnotations;

namespace PIAWebApi.Entidades
{
    public class Rifa
    {

        public int Id                                      { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        [StringLength(maximumLength: 80, ErrorMessage = "El máximo de caracteres para este campo son {1}")]
        public string NameRifa                             { get; set; }

        public List<RifasParticipantes> RifasParticipantes { get; set; }

    }

}
