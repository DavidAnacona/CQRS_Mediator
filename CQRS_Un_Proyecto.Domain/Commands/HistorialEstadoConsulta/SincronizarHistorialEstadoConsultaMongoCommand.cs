using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.HistorialEstadoConsulta
{
    public class SincronizarHistorialEstadoConsultaMongoCommand : IRequest<Unit>
    {
        public HistorialEstadoConsultaEntity HistorialEstadoConsulta { get; set; }

        public SincronizarHistorialEstadoConsultaMongoCommand(HistorialEstadoConsultaEntity historialEstadoConsulta)
        {
            HistorialEstadoConsulta = historialEstadoConsulta;
        }
    }
}
