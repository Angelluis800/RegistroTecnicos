using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class TiposTecnicos
{
    [Key]
    public int TipoId { get; set; }

    [Required(ErrorMessage = " Este campo debe ser llenado con los datos requeridos")]
    [RegularExpression(@"[a-zA-ZñÑ\s]+$", ErrorMessage = "Este campo no debe contener caracteres especiales ni números")]
    public string? Descripcion { get; set; }
    public List<Incentivos> Incentivos { get; set; } = new List<Incentivos>();
}
