using System;
using System.Collections.Generic;

namespace EmployeesWebApp.Models.Entities;

public partial class Puesto
{
    public int PuestoId { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
