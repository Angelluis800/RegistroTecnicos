using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class TipoTecnicos
{
    [Key]
    public int IdTecnico { get; set; }

    [Required(ErrorMessage = " Este campo debe ser llenado con los datos requeridos")]
    [RegularExpression(@"[a-zA-ZñÑ\s]+$", ErrorMessage = "Este campo no debe contener caracteres especiales ni números")]
    public string? Descripcion { get; set; }
}
