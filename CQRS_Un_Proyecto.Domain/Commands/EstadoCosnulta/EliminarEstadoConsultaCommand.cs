using MediatR;

namespace CQRS_Distribuidos.Infrastructure.Commands.EstadoConsulta
{
    public record EliminarEstadoConsultaCommand(int Id) : IRequest<bool>;
}
