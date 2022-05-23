using System.ComponentModel.DataAnnotations;

namespace PIAWebApi.DTO
{
    public class RifasCreacionDTO
    {

        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        [StringLength(maximumLength: 80, ErrorMessage = "El máximo de caracteres para este campo son 60")]
        public string NameRifa { get; set; }

        

    }
}
