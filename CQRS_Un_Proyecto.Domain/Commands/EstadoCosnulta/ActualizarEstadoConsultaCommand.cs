using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.EstadoConsulta
{
    public record ActualizarEstadoConsultaCommand(int IdEstadoConsulta, string NombreEstado) : IRequest<EstadoConsultaEntity>;
}
