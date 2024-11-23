using System;
using System.Collections.Generic;

namespace CQRS_Un_Proyecto.Infrastructure.Model;

public partial class Consultum
{
    public int IdConsulta { get; set; }

    public DateTime FechaHora { get; set; }

    public int IdPaciente { get; set; }

    public int IdMedico { get; set; }

    public int IdEstadoConsulta { get; set; }

    public string? Notas { get; set; }

    public virtual ICollection<HistorialEstadoConsultum> HistorialEstadoConsulta { get; set; } = new List<HistorialEstadoConsultum>();

    public virtual EstadoConsultum IdEstadoConsultaNavigation { get; set; } = null!;

    public virtual Medico IdMedicoNavigation { get; set; } = null!;

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;

    public virtual ICollection<Recetum> Receta { get; set; } = new List<Recetum>();
}
