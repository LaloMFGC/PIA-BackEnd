using PIAWebApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace PIAWebApi.DTO
{
    public class ParticipantesCreacionDTO
    {

        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        [StringLength(maximumLength: 80, ErrorMessage = "El máximo de caracteres para este campo son {1}")]
        public string Username { get; set; }

        public bool Ganador { get; set; }

        [Range(1, 54)]
        public int LoteriaId { get; set; }

        public string Correo { get; set; }

        [MaxLength(10)]
        [MinLength(10)]
        [ValidationPhone]
        public string Telefono { get; set; }

        public List<int> RifasIds { get; set; }


    }
}
