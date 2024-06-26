﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }

    [Required(ErrorMessage = " Este campo debe ser llenado con los datos requeridos")]
    [RegularExpression(@"[a-zA-ZñÑ\s]+$", ErrorMessage = "Este campo no debe contener caracteres especiales")]
    public string? Nombres { get; set; }

    [Required(ErrorMessage = "Este campo debe ser llenado con los datos requeridos")]
    [Range(0.1, 100000000, ErrorMessage = "Ingrese un sueldo mayor a 0 y menor a 100,000,000")]
    public decimal SueldoHora { get; set; }

    [ForeignKey("TiposTecnicos")]
    [Range(1, 50, ErrorMessage = "Seleccione un Tipo")]
    public int idTipo { get; set; }    
    public TiposTecnicos? TiposTecnicos { get; set; }

    [ForeignKey("Incentivos")]
    public int? IncentivoId { get; set; }   // Un Técnico Puede Contener o No un Incentivo por eso le agregamos el "?"//
    public Incentivos? Incentivos { get; set; }   
}
