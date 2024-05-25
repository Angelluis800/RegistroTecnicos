using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class Incentivos
{
    [Key]
    public int IncentivoId { get; set; }

    [Required(ErrorMessage ="Este Campo Debe ser Llenado")]
    [RegularExpression(@"[a-zA-ZñÑ\s]+$", ErrorMessage = "Este Campo no Debe Contener Caracteres Especiales")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "Debe Contener una Fecha")]
    [RegularExpression(@"\d{2}/\d{2}/\d{4}", ErrorMessage = "La fecha debe estar en formato dd-MM-yyyy")]
    public string? Fecha { get; set; }

    [Required(ErrorMessage = "Este Campo Debe Contener al Menos un Servicio a Completar")]
    [Range(1,100, ErrorMessage = "Este Campo debe Contener al Menos un Servicio y no más de 100 Servicios")]
    public int CantidadServicios { get; set; }

    [Required(ErrorMessage = "Este Campo Debe Contener al Menos un Servicio a Completar")]
    [Range(0.1, 1000000, ErrorMessage = "Este Campo debe Contener al Menos un Monto de 0.1")]
    public decimal Monto { get; set; }

    [ForeignKey("TiposTecnicos")]
    [Range(1, 50, ErrorMessage = "Seleccione un Tipo")]
    public int TipoId { get; set; }
    public TiposTecnicos? TiposTecnicos { get; set; }
}
