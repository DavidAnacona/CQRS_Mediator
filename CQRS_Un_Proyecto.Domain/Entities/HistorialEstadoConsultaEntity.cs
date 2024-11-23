using System;
using CQRS_Un_Proyecto.Infrastructure.Model;

namespace CQRS_Un_Proyecto.Domain.Entities
{
    public class HistorialEstadoConsultaEntity
    {
        public int IdHistorialEstado { get; set; }

        public int IdConsulta { get; set; }

        public int IdEstadoConsulta { get; set; }

        public DateTime FechaCambio { get; set; }

        public string? UsuarioResponsable { get; set; }

        public string? Comentario { get; set; }

        public HistorialEstadoConsultaEntity()
        {
        }

        public HistorialEstadoConsultaEntity(HistorialEstadoConsultum historialEstadoConsultum)
        {
            IdHistorialEstado = historialEstadoConsultum.IdHistorialEstado;
            IdConsulta = historialEstadoConsultum.IdConsulta;
            IdEstadoConsulta = historialEstadoConsultum.IdEstadoConsulta;
            FechaCambio = historialEstadoConsultum.FechaCambio;
            UsuarioResponsable = historialEstadoConsultum.UsuarioResponsable;
            Comentario = historialEstadoConsultum.Comentario;
        }
    }
}
