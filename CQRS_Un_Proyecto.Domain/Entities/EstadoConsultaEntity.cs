using System;
using System.Collections.Generic;
using CQRS_Un_Proyecto.Infrastructure.Model;

namespace CQRS_Un_Proyecto.Domain.Entities
{
    public class EstadoConsultaEntity
    {
        public int IdEstadoConsulta { get; set; }
        public string NombreEstado { get; set; } = null!;

        public EstadoConsultaEntity() { }

        public EstadoConsultaEntity(EstadoConsultum estadoConsultum)
        {
            IdEstadoConsulta = estadoConsultum.IdEstadoConsulta;
            NombreEstado = estadoConsultum.NombreEstado;
        }
    }
}
