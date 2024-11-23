using CQRS_Un_Proyecto.Domain.Entities;
using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.HistorialEstadoConsulta
{
    public record CrearHistorialEstadoConsultaCommand(
        int IdConsulta,
        int IdEstadoConsulta,
        DateTime FechaCambio,
        string? UsuarioResponsable,
        string? Comentario
    ) : IRequest<HistorialEstadoConsultaEntity>;
}
