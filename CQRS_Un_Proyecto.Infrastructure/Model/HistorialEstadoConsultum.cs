using System;
using System.Collections.Generic;

namespace CQRS_Un_Proyecto.Infrastructure.Model;

public partial class HistorialEstadoConsultum
{
    public int IdHistorialEstado { get; set; }

    public int IdConsulta { get; set; }

    public int IdEstadoConsulta { get; set; }

    public DateTime FechaCambio { get; set; }

    public string? UsuarioResponsable { get; set; }

    public string? Comentario { get; set; }

    public virtual Consultum IdConsultaNavigation { get; set; } = null!;

    public virtual EstadoConsultum IdEstadoConsultaNavigation { get; set; } = null!;
}
