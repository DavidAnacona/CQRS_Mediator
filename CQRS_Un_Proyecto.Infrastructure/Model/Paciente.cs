using System;
using System.Collections.Generic;

namespace CQRS_Un_Proyecto.Infrastructure.Model;

public partial class Paciente
{
    public int IdPaciente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateTime? FechaNacimiento { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<Consultum> Consulta { get; set; } = new List<Consultum>();
}
