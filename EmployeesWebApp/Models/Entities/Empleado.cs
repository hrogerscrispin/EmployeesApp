using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesWebApp.Models.Entities;

public partial class Empleado
{
    [Key]
    [Column(TypeName = "int")]
    public int EmpleadoId { get; set; }

   
    [Column(TypeName = "varchar(max)")]
    [DisplayName("Fotografia")]
    public string? Fotografia { get; set; }

    
    [NotMapped]
    [DisplayName("Subir imagen")]
    public IFormFile? formFile { get; set; }

   
    [Column(TypeName = "varchar(100)")]
    [DisplayName("Nombre")]
    public string? Nombre { get; set; }

    
    [Column(TypeName = "varchar(100)")]
    [DisplayName("Apellido")]
    public string? Apellido { get; set; }

    
    public int? PuestoId { get; set; }

    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [DisplayName("F. Nacimiento")]
    public DateTime? FechaNacimiento { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [DisplayName("F. Contratación")]
    public DateTime? FechaContratacion { get; set; }

    
    [Column(TypeName = "varchar(100)")]
    [DisplayName("Direccion")]
    public string? Direccion { get; set; }

    
    [Column(TypeName = "varchar(20)")]
    [DisplayName("No. Tel.")]
    public string? Telefono { get; set; }

    
    [Column(TypeName = "varchar(100)")]
    [DisplayName("Correo")]
    public string? CorreoElectronico { get; set; }

   
    public int? EstadoId { get; set; }

    public virtual Estado? Estado { get; set; }

    public virtual Puesto? Puesto { get; set; }
}
