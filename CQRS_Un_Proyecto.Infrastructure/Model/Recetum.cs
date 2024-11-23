using System;
using System.Collections.Generic;

namespace CQRS_Un_Proyecto.Infrastructure.Model;

public partial class Recetum
{
    public int IdReceta { get; set; }

    public int IdConsulta { get; set; }

    public string Medicamentos { get; set; } = null!;

    public string? Indicaciones { get; set; }

    public virtual Consultum IdConsultaNavigation { get; set; } = null!;
}
