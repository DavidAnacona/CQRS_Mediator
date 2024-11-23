using System;
using System.Collections.Generic;

namespace CQRS_Un_Proyecto.Infrastructure.Model;

public partial class EstadoConsultum
{
    public int IdEstadoConsulta { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Consultum> Consulta { get; set; } = new List<Consultum>();

    public virtual ICollection<HistorialEstadoConsultum> HistorialEstadoConsulta { get; set; } = new List<HistorialEstadoConsultum>();
}
