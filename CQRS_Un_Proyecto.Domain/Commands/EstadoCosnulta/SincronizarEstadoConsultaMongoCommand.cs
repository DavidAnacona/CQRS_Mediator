using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.EstadoConsulta
{
    public class SincronizarEstadoConsultaMongoCommand : IRequest<Unit>
    {
        public EstadoConsultaEntity EstadoConsulta { get; set; }

        public SincronizarEstadoConsultaMongoCommand(EstadoConsultaEntity estadoConsulta)
        {
            EstadoConsulta = estadoConsulta;
        }
    }
}
