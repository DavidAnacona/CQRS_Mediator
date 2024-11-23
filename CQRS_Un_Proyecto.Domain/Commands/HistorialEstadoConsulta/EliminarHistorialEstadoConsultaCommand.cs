using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.HistorialEstadoConsulta
{
    public record EliminarHistorialEstadoConsultaCommand(int Id) : IRequest<bool>;
}
